using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Context;
using TestTask.ViewModels;

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
        public DataProviderResult AddPatient(CreatePatientViewModel patientModel)
        {
            return new DataProviderResult { Succeeded = false, Errors = new List<string>() { "Временная заглушка на добавление" } };
            try
            {
                Patient patient = new Patient
                {
                    Birthday = patientModel.Birthday,
                    Name = patientModel.Name,
                    Patronymic = patientModel.Patronymic,
                    Sex = patientModel.Sex,
                    SNILS = patientModel.SNILS,
                    Surname = patientModel.Surname,
                };

                context.Patients.Add(patient);
                context.SaveChanges();
                return new DataProviderResult { Succeeded = true };
            }
            catch { }
            return new DataProviderResult { Succeeded = false, Errors = new List<string>() { "Ошибка при добавлении пациента в базу данных" } };
        }

    }


    public class DataProviderResult
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
