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


        public static void InsertRegister(Register Register)
        {
            string sql = "INSERT INTO Registers(RegisterName,Device) VALUES(@RegisterName,@Device)";
            DbParameter par1 = Database.AddParameter("KlantConnection", "@RegisterName", Register.Name);
            DbParameter par2 = Database.AddParameter("KlantConnection", "@RegisterName", Register.Name);

            Database.InsertData("KlantConnection", sql, par1, par2);
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
    }
}