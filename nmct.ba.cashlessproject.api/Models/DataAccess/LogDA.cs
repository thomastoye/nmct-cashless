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
    public class LogDA
    {
        public static List<ErrorLog> Get()
        {
            List<ErrorLog> list = new List<ErrorLog>();

            string sql = "SELECT * FROM ErrorLog";
            DbDataReader reader = Database.GetData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }


        public static int Insert(ErrorLog log)
        {
            string sql = "INSERT INTO ErrorLog VALUES(@RegisterID,@Timestamp,@Message, @Stacktrace)";

            DbParameter par1 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@RegisterID", log.RegisterID);
            DbParameter par2 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Timestamp", log.Timestamp);
            DbParameter par3 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Message", log.Message);
            DbParameter par4 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Stacktrace", log.StackTrace);

            return Database.InsertData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, par1, par2, par3, par4);
        }

        private static ErrorLog Create(IDataRecord record)
        {
            return new ErrorLog()
            {
                RegisterID = Int32.Parse(record["RegisterID"].ToString()),
                Timestamp = DateTime.Parse(record["Timestamp"].ToString()),
                Message = record["Message"].ToString(),
                StackTrace = record["Stacktrace"].ToString()
            };
        }
    }
}