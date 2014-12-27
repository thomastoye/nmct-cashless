using nmct.ba.cashlessproject.api.Models;
using nmct.ba.cashlessproject.api.Models.DataAccess;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.api.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RegisterManagerController : Controller
    {
        [HttpGet]
        public ActionResult AssignedToOrganisation(int id)
        {
            Organisation org = Organisations.GetById(id);
            if (org == null) return new HttpNotFoundResult();

            var registers = RegistersManagement.GetByOrganisationId(id);

            ViewBag.Organisation = org;
            return View(registers);
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.UnassignedRegisters = RegistersManagement.GetRegisters().FindAll(reg => reg.AssignedTo == null);
            return View(RegistersManagement.GetRegisters());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RegisterManagement reg)
        {
            if (ModelState.IsValid)
            {
                RegistersManagement.InsertRegister(reg);
                return RedirectToAction("Index");
            }
            else
            {
                return View(reg);
            }

            
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            RegistersManagement.DeleteRegister(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AssignTo(int id){
            // validate register ID
            RegisterManagement reg = RegistersManagement.GetById(id);
            if (reg == null) return new HttpNotFoundResult();

            ViewBag.Orgs = Organisations.Get();
            return View(new Organisation_RegisterBindModel() { 
                FromDate = DateTime.Now.Date, UntilDate = DateTime.Now.AddYears(5).Date, RegisterID = id
            });
        }

        [HttpPost]
        public ActionResult AssignTo(Organisation_RegisterBindModel model)
        {
            if (ModelState.IsValid && Organisations.GetById(model.OrganisationID) != null && RegistersManagement.GetById(model.RegisterID) != null)
            {
                AssignRegisterDA.Insert(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
    }
}