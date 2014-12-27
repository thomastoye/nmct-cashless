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

        public static List<RegisterOrganisation> Get(ConnectionStringSettings connectionString)
        {
            List<RegisterOrganisation> list = new List<RegisterOrganisation>();

            string sql = "SELECT ID, RegisterName, Device FROM Registers";
            DbDataReader reader = Database.GetData(connectionString, sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }


        public static int Insert(ConnectionStringSettings connectionString, RegisterOrganisation register)
        {
            string sql = "INSERT INTO Registers(RegisterName,Device) VALUES(@RegisterName,@Device)";
            if (register.Name == null) register.Name = "";
            if (register.Device == null) register.Device = "";

            DbParameter par1 = Database.AddParameter(connectionString, "@RegisterName", register.Name);
            DbParameter par2 = Database.AddParameter(connectionString, "@Device", register.Device);

            return Database.InsertData(connectionString, sql, par1, par2);
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

        public static void Update(ConnectionStringSettings connectionString, long id, RegisterOrganisation reg)
        {
            string sql = "UPDATE registers SET RegisterName=@RegisterName,Device=@DeviceName WHERE ID=@ID;";

            if (reg.Name == null) reg.Name = "";
            if (reg.Device == null) reg.Device = "";

            DbParameter regName = Database.AddParameter(connectionString, "@RegisterName", reg.Name);
            DbParameter regDeviceName = Database.AddParameter(connectionString, "@DeviceName", reg.Device);
            DbParameter regId = Database.AddParameter(connectionString, "@ID", id);

            Database.ModifyData(connectionString, sql, regName, regDeviceName, regId);
        }

        public static void Delete(ConnectionStringSettings connectionString, long id)
        {
            string sql = "DELETE FROM registers WHERE ID=@ID";

            DbParameter parId = Database.AddParameter(connectionString, "@ID", id);

            Database.ModifyData(connectionString, sql, parId);
        }
    }
}
