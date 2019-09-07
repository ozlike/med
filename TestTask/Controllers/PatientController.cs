using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTask.Context;
using TestTask.ViewModels;
using TestTask.Extensions;
using TestTask.Models;

namespace TestTask.Controllers
{
    public class PatientController : Controller
    {
        IDataProvider db;
        public PatientController(IDataProvider dataProvider)
        {
            db = dataProvider;
        }

        public async Task<IActionResult> Index(int? id)
        {
            var patient = await db.GetPatientWithGrafts(id);
            if (patient == null) return RedirectToAction("All", "Patient");
            return View(patient.CopyPropertyValuesTo(new PatientViewModel()));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var patient = await db.GetPatient(id);
            if (patient == null) return RedirectToAction("All", "Patient");

            return View(patient.CopyPropertyValuesTo(new PatientViewModel()));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await db.EditPatient(model);
                if (result.Succeeded)
                {
                    return View("ShowMessage", new ShowMessageModel {
                        Message = "Пользователь успешно отредактирован!",
                        Url = "/Patient/Index?id=" + model.Id,
                    });
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
        public async Task<IActionResult> Create(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await db.AddPatient(model);
                if (result.Succeeded)
                {
                    return View("ShowMessage", new ShowMessageModel
                    {
                        Message = "Пользователь успешно добавлен!",
                        Url = "/Patient/Index?id=" + (result.ReturnedData as int?),
                    });
                }

                ViewBag.Errors = result.Errors;

                return View(model);
            }

            return View(model);
        }

        public async Task<IActionResult> All()
        {
            return View(await db.GetAllPatients());
        }
    }
}