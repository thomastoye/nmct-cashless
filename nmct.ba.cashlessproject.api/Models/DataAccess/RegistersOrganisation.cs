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
    public class RegistersOrganisation
    {
        
        public static List<RegisterOrganisation> GetRegisters()
        {
            List<RegisterOrganisation> list = new List<RegisterOrganisation>();

            string sql = "SELECT ID, RegisterName, Device FROM Registers";
            DbDataReader reader = Database.GetData(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }


        public static int InsertRegister(RegisterOrganisation register)
        {
            string sql = "INSERT INTO Registers(RegisterName,Device) VALUES(@RegisterName,@Device)";
            if (register.Name == null) register.Name = "";
            if (register.Device == null) register.Device = "";

            DbParameter par1 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@RegisterName", register.Name);
            DbParameter par2 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@Device", register.Device);

            return Database.InsertData(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], sql, par1, par2);
        }

        private static RegisterOrganisation Create(IDataRecord record)
        {
            return new RegisterOrganisation()
            {
                ID = Int32.Parse(record["ID"].ToString()),
                Name = record["RegisterName"].ToString(),
                Device = record["Device"].ToString()
            };
        }

        /**
         * This method updates a register
         * It takes an id and a new register. It will update the record in the database with that id to match the given register
         */
        public static void UpdateRegister(long id, RegisterOrganisation reg)
        {
            string sql = "UPDATE registers SET RegisterName=@RegisterName,Device=@DeviceName WHERE ID=@ID;";

            if (reg.Name == null) reg.Name = "";
            if (reg.Device == null) reg.Device = "";

            DbParameter regName = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@RegisterName", reg.Name);
            DbParameter regDeviceName = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@DeviceName", reg.Device);
            DbParameter regId = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@ID", id);

            Database.ModifyData(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], sql, regName, regDeviceName, regId);
        }

        public static void DeleteRegister(long id)
        {
            string sql = "DELETE FROM registers WHERE ID=@ID";

            DbParameter parId = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], "@ID", id);

            Database.ModifyData(ConfigurationManager.AppSettings["ConnectionStringOrganisation"], sql, parId);
        }
    }
}
