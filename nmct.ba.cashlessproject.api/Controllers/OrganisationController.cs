using nmct.ba.cashlessproject.api.Models;
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
        public ActionResult Edit(int id)
        {
            Organisation org = Organisations.GetById(id);
            if (org == null) return new HttpNotFoundResult();
            return View(org);
        }

        [HttpPost]
        public ActionResult Edit(Organisation org)
        {
            Organisation testIfExists = Organisations.GetById(org.ID);
            if (testIfExists == null) return new HttpNotFoundResult();

            Organisations.Update(org);
            return RedirectToAction("Details", new { id = org.ID });
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Organisation org = Organisations.GetById(id);
            if (org == null) return new HttpNotFoundResult();
            return View(org);
        }
    }
}