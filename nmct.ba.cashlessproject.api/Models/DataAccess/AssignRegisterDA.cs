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
        public static void Insert(Organisation_RegisterBindModel register)
        {
            Organisation org = Organisations.GetById(register.OrganisationID);
            RegisterManagement registerFromDatabase = RegistersManagement.GetById(register.RegisterID);
            if (org == null || registerFromDatabase == null) return;

            string sql = "INSERT INTO Organisation_Register VALUES(@OrganisationID, @RegisterID, @FromDate, @UntilDate)";

            DbParameter par0 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@OrganisationID", register.OrganisationID);
            DbParameter par1 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@RegisterID", register.RegisterID);
            DbParameter par2 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@FromDate", register.FromDate);
            DbParameter par3 = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@UntilDate", register.UntilDate);

            Database.InsertData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, par0, par1, par2, par3);

            sql = "INSERT INTO Registers VALUES(@ID, @RegisterName, @Device)";
            ConnectionStringSettings settings = new ConnectionStringSettings("KlantDynamicConnection", org.DatabaseConnectionString, "System.Data.SqlClient"); ;

            DbParameter parRegisterName = Database.AddParameter(settings, "@RegisterName", registerFromDatabase.Name);
            DbParameter parID = Database.AddParameter(settings, "@ID", registerFromDatabase.ID);
            DbParameter parRegisterDevice = Database.AddParameter(settings, "@Device", registerFromDatabase.Device);

            Database.InsertData(settings, sql, parRegisterDevice, parRegisterName, parID);
        }

        public static void DeleteRegister(int id)
        {
            RegisterManagement register = RegistersManagement.GetById(id);

            string sql = "DELETE FROM Organisation_Register WHERE RegisterID=@ID";

            DbParameter parId = Database.AddParameter(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], "@ID", id);

            Database.ModifyData(ConfigurationManager.AppSettings["ConnectionStringItBedrijf"], sql, parId);

            // Delete the register from the client database
            if (register.AssignedTo != null)
            {
                sql = "DELETE FROM Registers WHERE ID=@ID";

                ConnectionStringSettings settings = new ConnectionStringSettings("KlantDynamicConnection", register.AssignedTo.DatabaseConnectionString, "System.Data.SqlClient"); ;

                DbParameter parRegisterID = Database.AddParameter(settings, "@ID", register.ID);

                Database.ModifyData(settings, sql, parRegisterID);
            }
        }
    }
}