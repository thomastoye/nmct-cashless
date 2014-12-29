using nmct.ba.cashlessproject.api.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using nmct.ba.cashlessproject.model;
using System.Configuration;
using nmct.ba.cashlessproject.common;
using System.IO;

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
                Password = Cryptography.Decrypt(record["Password"].ToString()),
                DbPassword = Cryptography.Decrypt(record["DbPassword"].ToString()),
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
            string sql = "INSERT INTO Organisations VALUES(@Login, @Password, @DbLogin, @DbPassword, @OrganisationName, @Address, @Email, @Phone)";

            DbParameter par0 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Login", org.Login);
            DbParameter par1 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Password", Cryptography.Encrypt(org.Password));
            DbParameter par3 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@DbPassword", Cryptography.Encrypt(org.DbPassword));
            DbParameter par4 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@OrganisationName", org.OrganisationName);
            DbParameter par5 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Address", org.Address);
            DbParameter par6 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Email", org.Email);
            DbParameter par7 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Phone", org.Phone);
            DbParameter par8 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@ID", org.ID);
            DbParameter par9 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@DbLogin", org.DbLogin);

            CreateDatabase(org);

            return Database.InsertData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, par0, par1, /*par2,*/ par3, par4, par5, par6, par7, par8, par9);
        }

        public static void Update(Organisation org)
        {
            string sql = "UPDATE Organisations SET Login=@Login, Password=@Password, DbLogin=@DbLogin, DbPassword=@DbPassword, OrganisationName=@OrganisationName, Address=@Address, Email=@Email, Phone=@Phone WHERE ID=@ID";

            DbParameter par0 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Login", org.Login);
            DbParameter par1 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Password", Cryptography.Encrypt(org.Password));
            DbParameter par3 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@DbPassword", Cryptography.Encrypt(org.DbPassword));
            DbParameter par4 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@OrganisationName", org.OrganisationName);
            DbParameter par5 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Address", org.Address);
            DbParameter par6 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Email", org.Email);
            DbParameter par7 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Phone", org.Phone);
            DbParameter par8 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@ID", org.ID);
            DbParameter par9 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@DbLogin", org.DbLogin);

            Database.ModifyData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, par0, par1, /*par2,*/ par3, par4, par5, par6, par7, par8, par9);
        }

        public static Organisation TryLogin(string username, string password)
        {
            string sql = "SELECT * FROM Organisations WHERE Login=@Login AND Password=@Password";

            DbParameter parUser = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Login", username);
            DbParameter parPass = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Password", Cryptography.Encrypt(password));

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

        public static Organisation GetById(int id)
        {
            string sql = "SELECT * FROM Organisations WHERE Id=@Id";

            DbParameter parLogin = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@ID", id);

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

            DbParameter parPass = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@Password", Cryptography.Encrypt(newPassword));
            DbParameter parId = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@ID", id);

            Database.ModifyData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, parPass, parId);
        }

        private static void CreateDatabase(Organisation o)
        {
            // create the actual database
            string create = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "CreateOrgDatabase.txt");
            string sql = create.Replace("@@DatabaseName", o.DbName).Replace("@@DatabaseLogin", o.DbLogin).Replace("@@DatabasePassword", o.DbPassword);
            foreach (string commandText in RemoveGo(sql))
            {
                Database.ModifyData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], commandText);
            }

            // create login, user and tables
            DbTransaction trans = null;
            try
            {
                trans = Database.BeginTransaction(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"]);

                string fill = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "FillOrgDatabase.txt");
                string sql2 = fill.Replace("@@DatabaseName", o.DbName).Replace("@@DatabaseLogin", o.DbLogin).Replace("@@DatabasePassword", o.DbPassword);

                foreach (string commandText in RemoveGo(sql2))
                {
                    Database.ModifyData(trans, commandText);
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Console.WriteLine(ex.Message);
            }
        }

        private static string[] RemoveGo(string input)
        {
            //split the script on "GO" commands
            string[] splitter = new string[] { "\r\nGO\r\n" };
            string[] commandTexts = input.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            return commandTexts;
        }
    }
}