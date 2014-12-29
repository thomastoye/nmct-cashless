using nmct.ba.cashlessproject.api.Helpers;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models.DataAccess
{
    public class RegistersManagement
    {
        public static List<RegisterManagement> GetRegisters()
        {
            List<RegisterManagement> list = new List<RegisterManagement>();

            string sql = "SELECT reg.*, joinTable.OrganisationID FROM Registers AS reg LEFT JOIN Organisation_Register AS joinTable ON reg.ID = joinTable.RegisterID WHERE (joinTable.FromDate < GETDATE() AND joinTable.UntilDate > GETDATE()) OR (joinTable.FromDate IS NULL)";
            DbDataReader reader = Database.GetData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }


        public static int InsertRegister(RegisterManagement register)
        {
            string sql = "INSERT INTO Registers(RegisterName,Device,PurchaseDate,ExpiresDate) VALUES(@RegisterName,@Device, @Purchased, @Expires)";
            if (register.Name == null) register.Name = "";
            if (register.Device == null) register.Device = "";

            DbParameter par1 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@RegisterName", register.Name);
            DbParameter par2 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Device", register.Device);
            DbParameter par3 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Purchased", register.PurchaseDate);
            DbParameter par4 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Expires", register.ExpiresDate);

            return Database.InsertData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, par1, par2, par3, par4);
        }

        private static RegisterManagement Create(IDataRecord record)
        {
            Organisation org = null;
            if (DBNull.Value != record["OrganisationID"])
            {
                org = Organisations.GetById(int.Parse(record["OrganisationID"].ToString()));
            }

            return new RegisterManagement()
            {
                ID = Int32.Parse(record["ID"].ToString()),
                Name = record["RegisterName"].ToString(),
                Device = record["Device"].ToString(),
                PurchaseDate = DateTime.Parse(record["PurchaseDate"].ToString()),
                ExpiresDate = DateTime.Parse(record["ExpiresDate"].ToString()),
                AssignedTo = org
            };
        }

        public static void DeleteRegister(int id)
        {
            AssignRegisterDA.DeleteRegister(id); // remove all assignments to organisations

            string sql = "DELETE FROM registers WHERE ID=@ID";

            DbParameter parId = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@ID", id);

            Database.ModifyData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, parId);
        }

        public static RegisterManagement GetById(int id)
        {
            string sql = "SELECT reg.*, joinTable.OrganisationID FROM Registers AS reg LEFT JOIN Organisation_Register AS joinTable ON reg.ID = joinTable.RegisterID WHERE ((joinTable.FromDate < GETDATE() AND joinTable.UntilDate > GETDATE()) OR (joinTable.FromDate IS NULL)) AND reg.ID=@ID";

            DbParameter parId = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@ID", id);

            DbDataReader reader = Database.GetData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, parId);

            RegisterManagement res;

            if (!reader.Read())
                res = null;
            else
                res = Create(reader);

            reader.Close();
            return res;
        }

        public static List<RegisterManagement> GetByOrganisationId(int id)
        {
            List<RegisterManagement> list = new List<RegisterManagement>();

            string sql = "SELECT reg.*, joinTable.OrganisationID FROM Registers AS reg LEFT JOIN Organisation_Register AS joinTable ON reg.ID = joinTable.RegisterID WHERE ((joinTable.FromDate < GETDATE() AND joinTable.UntilDate > GETDATE()) OR (joinTable.FromDate IS NULL)) AND OrganisationID=@ID";

            DbParameter parOrgId = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@ID", id);
            
            DbDataReader reader = Database.GetData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, parOrgId);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }
    }
}