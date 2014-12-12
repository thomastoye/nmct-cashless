using nmct.ba.cashlessproject.api.Models.DataAccess;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class OrganisationController : Controller
    {
        
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            ViewBag.Orgs = Organisations.Get();
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Organisation org)
        {
            Organisations.Insert(org);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Organisations.Delete(id);
            return RedirectToAction("Index");
        }
    }
}