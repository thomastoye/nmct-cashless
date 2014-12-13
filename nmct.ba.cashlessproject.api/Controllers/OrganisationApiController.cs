using nmct.ba.cashlessproject.api.Models.DataAccess;
using System.Web.Http;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class OrganisationApiController : ApiController
    {

        [Authorize(Roles = "OrganisationManager")]
        [HttpPost]
        public object ChangePassword(string oldPassword, string newPassword)
        {
            if (oldPassword == null || newPassword == null || oldPassword == "" || newPassword == "")
                return "false";

            if (Organisations.TryLogin(User.Identity.Name, oldPassword) == null)
                return "false";

            var org = Organisations.GetByUser(User.Identity.Name);

            if (org == null)
                return "false";

            Organisations.ChangePassword(org.ID, newPassword);

            return "true";
        }
    }
}