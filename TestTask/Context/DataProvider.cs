using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Context;
using TestTask.ViewModels;
using TestTask.Extensions;
using TestTask.Models;

namespace TestTask.Context
{
    public class DataProvider : IDataProvider
    {
        MedContext context;
        public DataProvider(MedContext context)
        {
            this.context = context;
        }

        public async Task<DataProviderResult> AddGraft(GraftViewModel graftModel)
        {
            try
            {
                var patient = await GetPatient(graftModel.PatientId);
                if (patient == null) return new DataProviderResult { Succeeded = false, Errors = new List<string>() { "Пользователь не найден" } };

                Graft graft = new Graft
                {
                    Drug = graftModel.Drug,
                    EventDate = graftModel.EventDate,
                    Consent = graftModel.Consent,
                    Patient = patient,
                };

                await context.Grafts.AddAsync(graft);
                await context.SaveChangesAsync();
                return new DataProviderResult { Succeeded = true, ReturnedData = patient.Id };
            }
            catch { }
            return new DataProviderResult { Succeeded = false, Errors = new List<string>() { "Ошибка при добавлении прививки в базу данных" } };
        }

        public async Task<DataProviderResult> AddPatient(PatientViewModel patientModel)
        {
            try
            {
                var patient = new Patient
                {
                    Birthday = patientModel.Birthday,
                    Name = patientModel.Name,
                    Patronymic = patientModel.Patronymic,
                    Sex = (SexType)patientModel.Sex,
                    SNILS = SnilsUniversalView(patientModel.SNILS),
                    Surname = patientModel.Surname,
                };

                await context.Patients.AddAsync(patient);
                await context.SaveChangesAsync();
                return new DataProviderResult { Succeeded = true, ReturnedData = patient.Id };
            }
            catch { }
            return new DataProviderResult { Succeeded = false, Errors = new List<string>() { "Ошибка при добавлении пациента в базу данных" } };
        }

        public async Task<DataProviderResult> EditPatient(PatientViewModel patientModel)
        {
            try
            {
                var patient = await GetPatient(patientModel.Id);
                if (patient == null) return new DataProviderResult { Succeeded = false, Errors = new List<string>() { "Пациент не найден" } };

                patientModel.CopyPropertyValuesTo(patient);
                await context.SaveChangesAsync();
                return new DataProviderResult { Succeeded = true };
            }
            catch { }
            return new DataProviderResult { Succeeded = false, Errors = new List<string>() { "Ошибка при редактировании пациента" } };
        }

        public async Task<ICollection<Patient>> GetAllPatients()
        {
            return await context.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatient(int? id)
        {
            if (!id.HasValue) return null;
            return await context.Patients.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Patient> GetPatientWithGrafts(int? id)
        {
            if (!id.HasValue) return null;
            return await context.Patients.Where(x => x.Id == id).Include(x => x.Grafts).FirstOrDefaultAsync();
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
        public object ReturnedData { get; set; } = null;
    }
}
