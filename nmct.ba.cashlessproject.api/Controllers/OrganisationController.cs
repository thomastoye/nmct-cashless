using nmct.ba.cashlessproject.api.Models.DataAccess;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.api.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OrganisationController : Controller
    {
        
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [HttpGet]
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
            if (ModelState.IsValid) { 
                Organisations.Insert(org);
                return RedirectToAction("Index");
            }
            else
            {
                return View(org);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Organisations.Delete(id);
            return RedirectToAction("Index");
        }
    }
}