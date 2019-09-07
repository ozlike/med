using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTask.Context;
using TestTask.ViewModels;
using TestTask.Extensions;

namespace TestTask.Controllers
{
    public class PatientController : Controller
    {
        IDataProvider db;
        public PatientController(IDataProvider dataProvider)
        {
            db = dataProvider;
        }

        public IActionResult Index(int? id)
        {
            var patient = db.GetPatientWithGrafts(id);
            if (patient == null) return RedirectToAction("All", "Patient");
            return View(patient.CopyPropertyValuesTo(new PatientViewModel()));
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var patient = db.GetPatient(id);
            if (patient == null) return RedirectToAction("All", "Patient");

            return View(patient.CopyPropertyValuesTo(new PatientViewModel()));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = db.EditPatient(model);
                if (result.Succeeded)
                {
                    return Content("Edited!");
                }

                ViewBag.Errors = result.Errors;

                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = db.AddPatient(model);
                if (result.Succeeded)
                {
                    //return RedirectToAction("Patient");
                    return Content("Added!");
                }

                ViewBag.Errors = result.Errors;
                //result.Errors.ForEach(x => ModelState.AddModelError("", x));

                return View(model);
            }

            return View(model);
        }

        public IActionResult All()
        {
            return View(db.GetAllPatients());
        }
    }
}