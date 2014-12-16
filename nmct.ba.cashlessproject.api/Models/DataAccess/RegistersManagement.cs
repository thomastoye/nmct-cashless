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

            string sql = "SELECT * FROM Registers";
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
            DbParameter par3 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Purchased", register.Name);
            DbParameter par4 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Expires", register.Device);

            return Database.InsertData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, par1, par2, par3, par4);
        }

        private static RegisterManagement Create(IDataRecord record)
        {
            return new RegisterManagement()
            {
                ID = Int32.Parse(record["ID"].ToString()),
                Name = record["RegisterName"].ToString(),
                Device = record["Device"].ToString(),
                PurchaseDate = DateTime.Parse(record["PurchaseDate"].ToString()),
                ExpiresDate = DateTime.Parse(record["ExpiresDate"].ToString())
            };
        }

        public static void UpdateRegister(long id, RegisterManagement reg)
        {
            string sql = "UPDATE registers SET RegisterName=@RegisterName,Device=@DeviceName,PurchaseDate=@Purchased,ExpiresDate=@Expires WHERE ID=@ID;";

            DbParameter regName = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@RegisterName", reg.Name);
            DbParameter regDeviceName = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@DeviceName", reg.Device);
            DbParameter regId = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@ID", id);

            Database.ModifyData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, regName, regDeviceName, regId);
        }

        public static void DeleteRegister(long id)
        {
            string sql = "DELETE FROM registers WHERE ID=@ID";

            DbParameter parId = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@ID", id);

            Database.ModifyData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, parId);
        }
    }
}