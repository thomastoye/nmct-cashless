using nmct.ba.cashlessproject.api.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

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
                Phone = record["Phone"].ToString()
            };
        }

        public List<Organisation> Get()
        {
            string sql = "SELECT * FROM Organisations";
            DbDataReader reader = Database.GetData("ITBedrijf", sql);

            List<Organisation> list = new List<Organisation>();

            while (reader.Read())
                list.Add(Create(reader));

            return list;
        }

        public static int Insert(Organisation org)
        {
            string sql = "INSERT INTO Organisations VALUES(@ID, @Login, @Password, @DbName, @DbPassword, @OrganisationName, @Address, @Email, @Phone)";

            DbParameter par0 = Database.AddParameter("ITBedrijf", "@Login", org.Login);
            DbParameter par1 = Database.AddParameter("ITBedrijf", "@Password", org.Password);
            DbParameter par2 = Database.AddParameter("ITBedrijf", "@DbName", org.DbName);
            DbParameter par3 = Database.AddParameter("ITBedrijf", "@DbPassword", org.DbPassword);
            DbParameter par4 = Database.AddParameter("ITBedrijf", "@OrganisationName", org.OrganisationName);
            DbParameter par5 = Database.AddParameter("ITBedrijf", "@Address", org.Address);
            DbParameter par6 = Database.AddParameter("ITBedrijf", "@Email", org.Email);
            DbParameter par7 = Database.AddParameter("ITBedrijf", "@Phone", org.Phone);
            DbParameter par8 = Database.AddParameter("ITBedrijf", "@ID", org.ID);

            return Database.InsertData("ITBedrijf", sql, par0, par1, par2, par3, par4, par5, par6, par7, par8);
        }

        public static void Delete(int id)
        {
            string sql = "DELETE FROM Organisations WHERE ID=@ID";

            DbParameter parId = Database.AddParameter("ITBedrijf", "@ID", id);

            Database.ModifyData("ITBedrijf", sql, parId);
        }

        public static void update(Organisation org)
        {
            string sql = "UPDATE Organisations SET ID=@ID, Login=@Login, Password=@Password, DbName=@DbName, DbPassword=@DbPassword, OrganisationName=@OrganisationName, Address=@Address, Email=@Email, Phone=@Phone WHERE ID=@ID";

            DbParameter par0 = Database.AddParameter("ITBedrijf", "@Login", org.Login);
            DbParameter par1 = Database.AddParameter("ITBedrijf", "@Password", org.Password);
            DbParameter par2 = Database.AddParameter("ITBedrijf", "@DbName", org.DbName);
            DbParameter par3 = Database.AddParameter("ITBedrijf", "@DbPassword", org.DbPassword);
            DbParameter par4 = Database.AddParameter("ITBedrijf", "@OrganisationName", org.OrganisationName);
            DbParameter par5 = Database.AddParameter("ITBedrijf", "@Address", org.Address);
            DbParameter par6 = Database.AddParameter("ITBedrijf", "@Email", org.Email);
            DbParameter par7 = Database.AddParameter("ITBedrijf", "@Phone", org.Phone);
            DbParameter par8 = Database.AddParameter("ITBedrijf", "@ID", org.ID);

            Database.ModifyData("ITBedrijf", sql, par0, par1, par2, par3, par4, par5, par6, par7, par8);
        }
    }
}