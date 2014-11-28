using nmct.ba.cashlessproject.api.Helpers;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models.DataAccess
{
    public class Registers
    {
        public static List<Register> GetRegisters()
        {
            List<Register> list = new List<Register>();

            string sql = "SELECT ID, RegisterName, Device FROM Registers";
            DbDataReader reader = Database.GetData("KlantConnection", sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }


        public static int InsertRegister(Register register)
        {
            string sql = "INSERT INTO Registers(RegisterName,Device) VALUES(@RegisterName,@Device)";
            if (register.Name == null) register.Name = "";
            if (register.Device == null) register.Device = "";

            DbParameter par1 = Database.AddParameter("KlantConnection", "@RegisterName", register.Name);
            DbParameter par2 = Database.AddParameter("KlantConnection", "@Device", register.Device);

            return Database.InsertData("KlantConnection", sql, par1, par2);
        }

        private static Register Create(IDataRecord record)
        {
            return new Register()
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
        public static void UpdateRegister(long id, Register reg)
        {
            string sql = "UPDATE registers SET RegisterName=@RegisterName,Device=@DeviceName WHERE ID=@ID;";

            if (reg.Name == null) reg.Name = "";
            if (reg.Device == null) reg.Device = "";

            DbParameter regName = Database.AddParameter("KlantConnection", "@RegisterName", reg.Name);
            DbParameter regDeviceName = Database.AddParameter("KlantConnection", "@DeviceName", reg.Device);
            DbParameter regId = Database.AddParameter("KlantConnection", "@ID", id);

            Database.ModifyData("KlantConnection", sql, regName, regDeviceName, regId);
        }

        public static void Delete(long id)
        {
            string sql = "DELETE FROM registers WHERE ID=@ID";

            DbParameter parId = Database.AddParameter("KlantConnection", "@ID", id);

            Database.ModifyData("KlantConnection", sql, parId);
        }
    }
}