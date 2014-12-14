using nmct.ba.cashlessproject.api.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using nmct.ba.cashlessproject.model;
using System.Configuration;

namespace nmct.ba.cashlessproject.api.Models.DataAccess
{
    public class Organisations
    {
        private static Organisation Create(IDataRecord record)
        {
            return new Organisation()
            {
                ID = Int32.Parse(record["ID"].ToString()),
                Login = record["Login"].ToString(),
                Password = record["Password"].ToString(),
                DbName = record["DbName"].ToString(),
                DbPassword = record["DbPassword"].ToString(),
                OrganisationName = record["OrganisationName"].ToString(),
                Address = record["Address"].ToString(),
                Email = record["Email"].ToString(),
                Phone = record["Phone"].ToString(),
                DbLogin = record["DbLogin"].ToString()
            };
        }

        public static List<Organisation> Get()
        {
            string sql = "SELECT * FROM Organisations";
            DbDataReader reader = Database.GetData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql);

            List<Organisation> list = new List<Organisation>();

            while (reader.Read())
                list.Add(Create(reader));

            return list;
        }

        public static int Insert(Organisation org)
        {
            string sql = "INSERT INTO Organisations VALUES(@Login, @Password, @DbName, @DbLogin, @DbPassword, @OrganisationName, @Address, @Email, @Phone)";

            DbParameter par0 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Login", org.Login);
            DbParameter par1 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Password", org.Password);
            DbParameter par2 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@DbName", org.DbName);
            DbParameter par3 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@DbPassword", org.DbPassword);
            DbParameter par4 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@OrganisationName", org.OrganisationName);
            DbParameter par5 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Address", org.Address);
            DbParameter par6 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Email", org.Email);
            DbParameter par7 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Phone", org.Phone);
            DbParameter par8 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@ID", org.ID);
            DbParameter par9 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@DbLogin", org.DbLogin);

            return Database.InsertData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, par0, par1, par2, par3, par4, par5, par6, par7, par8, par9);
        }

        public static void Delete(int id)
        {
            string sql = "DELETE FROM Organisations WHERE ID=@ID";

            DbParameter parId = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@ID", id);

            Database.ModifyData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, parId);
        }

        public static void Update(Organisation org)
        {
            string sql = "UPDATE Organisations SET ID=@ID, Login=@Login, Password=@Password, DbName=@DbName, DbLogin=@DbLogin, DbPassword=@DbPassword, OrganisationName=@OrganisationName, Address=@Address, Email=@Email, Phone=@Phone WHERE ID=@ID";

            DbParameter par0 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Login", org.Login);
            DbParameter par1 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Password", org.Password);
            DbParameter par2 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@DbName", org.DbName);
            DbParameter par3 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@DbPassword", org.DbPassword);
            DbParameter par4 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@OrganisationName", org.OrganisationName);
            DbParameter par5 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Address", org.Address);
            DbParameter par6 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Email", org.Email);
            DbParameter par7 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Phone", org.Phone);
            DbParameter par8 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@ID", org.ID);
            DbParameter par9 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@DbLogin", org.DbLogin);

            Database.ModifyData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, par0, par1, par2, par3, par4, par5, par6, par7, par8, par9);
        }

        public static Organisation TryLogin(string username, string password)
        {
            string sql = "SELECT * FROM Organisations WHERE Login=@Login AND Password=@Password";

            DbParameter parUser = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Login", username);
            DbParameter parPass = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Password", password);

            DbDataReader reader = Database.GetData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, parUser, parPass);

            Organisation res;

            if (!reader.Read())
                res = null;
            else
                res = Create(reader);

            reader.Close();
            return res;
        }

        public static Organisation GetByUser(string name)
        {
            string sql = "SELECT * FROM Organisations WHERE Login=@Login";

            DbParameter parLogin = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Login", name);

            DbDataReader reader = Database.GetData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, parLogin);

            Organisation res;

            if (!reader.Read())
                res = null;
            else
                res = Create(reader);

            reader.Close();
            return res;

        }

        public static void ChangePassword(int id, string newPassword)
        {
            string sql = "UPDATE Organisations SET Password=@Password WHERE ID=@ID";

            DbParameter parPass = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Password", newPassword);
            DbParameter parId = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@ID", id);

            Database.ModifyData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, parPass, parId);
        }
    }
}