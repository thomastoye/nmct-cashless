using nmct.ba.cashlessproject.api.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models.DataAccess
{
    public class AssignRegisterDA
    {
        public static int Insert(Organisation_RegisterBindModel register)
        {
            string sql = "INSERT INTO Organisation_Register VALUES(@OrganisationID, @RegisterID, @FromDate, @UntilDate)";

            DbParameter par0 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@OrganisationID", register.OrganisationID);
            DbParameter par1 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@RegisterID", register.RegisterID);
            DbParameter par2 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@FromDate", register.FromDate);
            DbParameter par3 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@UntilDate", register.UntilDate);

            return Database.InsertData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, par0, par1, par2, par3);
        }

        public static void DeleteRegister(int id)
        {
            string sql = "DELETE FROM Organisation_Register WHERE RegisterID=@ID";

            DbParameter parId = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@ID", id);

            Database.ModifyData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, parId);
        }
    }
}