using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTask.Context;
using TestTask.Extensions;
using TestTask.Models;
using TestTask.ViewModels;

namespace TestTask.Controllers
{
    public class GraftController : Controller
    {
        IDataProvider db;
        public GraftController(IDataProvider dataProvider)
        {
            db = dataProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Add(int? patientId)
        {
            var patient = await db.GetPatient(patientId);
            if (patient == null) return RedirectToAction("All", "Patient");
            
            return View(new GraftViewModel
            {
                PatientId = (int)patientId,
                PatientFullName = $"{patient.Surname} {patient.Name} {patient.Patronymic}",
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(GraftViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await db.AddGraft(model);
                if (result.Succeeded)
                {
                    return View("ShowMessage", new ShowMessageModel
                    {
                        Message = "Прививка успешно добавлена!",
                        Url = "/Patient/Index?id=" + (result.ReturnedData as int?),
                    });
                }
                
                if (result.ReturnedData == null) return RedirectToAction("All", "Patient");

                ViewBag.Errors = result.Errors;

                return View(model);
            }

            return View(model);
        }
    }
}