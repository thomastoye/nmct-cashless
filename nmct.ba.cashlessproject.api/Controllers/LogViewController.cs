using nmct.ba.cashlessproject.api.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.api.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LogViewController : Controller
    {
        // GET: LogView
        public ActionResult Index()
        {
            return View(LogDA.Get());
        }
    }
}