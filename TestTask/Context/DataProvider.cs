using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Context;
using TestTask.ViewModels;
using TestTask.Extensions;

namespace TestTask.Context
{
    public class DataProvider : IDataProvider
    {
        MedContext context;
        public DataProvider(MedContext context)
        {
            this.context = context;

        }

        [ValidateAntiForgeryToken]
        public DataProviderResult AddPatient(PatientViewModel patientModel)
        {
            try
            {
                Patient patient = new Patient
                {
                    Birthday = patientModel.Birthday,
                    Name = patientModel.Name,
                    Patronymic = patientModel.Patronymic,
                    Sex = patientModel.Sex,
                    SNILS = SnilsUniversalView(patientModel.SNILS),
                    Surname = patientModel.Surname,
                };

                context.Patients.Add(patient);
                context.SaveChanges();
                return new DataProviderResult { Succeeded = true, AdditionalData = patient.Id };
            }
            catch { }
            return new DataProviderResult { Succeeded = false, Errors = new List<string>() { "Ошибка при добавлении пациента в базу данных" } };
        }

        public DataProviderResult EditPatient(PatientViewModel patientModel)
        {
            try
            {
                var patient = GetPatient(patientModel.Id);
                if (patient == null) return new DataProviderResult { Succeeded = false, Errors = new List<string>() { "Пациент не найден" } };

                patientModel.CopyPropertyValuesTo(patient);
                context.SaveChanges();
                return new DataProviderResult { Succeeded = true };
            }
            catch { }
            return new DataProviderResult { Succeeded = false, Errors = new List<string>() { "Ошибка при редактировании пациента" } };
        }

        public ICollection<Patient> GetAllPatients()
        {
            return context.Patients.ToList();
        }

        public Patient GetPatient(int? id)
        {
            if (!id.HasValue) return null;
            return context.Patients.Where(x => x.Id == id).FirstOrDefault();
        }

        public Patient GetPatientWithGrafts(int? id)
        {
            if (!id.HasValue) return null;
            return context.Patients.Where(x => x.Id == id).Include(x => x.Grafts).FirstOrDefault();
        }

        private string SnilsUniversalView(string val)
        {
            return string.Join("", val.Where(x => char.IsDigit(x))).Insert(3, "-").Insert(7, "-").Insert(11, " ");            
        }
    }


    public class DataProviderResult
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public object AdditionalData { get; set; } = null;
    }
}
