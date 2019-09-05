using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTask.Context;
using TestTask.ViewModels;

namespace TestTask.Controllers
{
    public class PatientController : Controller
    {
        IDataProvider db;
        public PatientController(IDataProvider dataProvider)
        {
            db = dataProvider;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatePatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = db.AddPatient(model);
                if (result.Succeeded)
                {
                    //return RedirectToAction("Patient");
                    return Content("Added!");
                }

                result.Errors.ForEach(x => ModelState.AddModelError("", x));

               // ModelState.AddModelError("", )

                return View(model);
            }

            return View(model);
        }
    }
}