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
    public class RegisterManagerController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
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
    }
}