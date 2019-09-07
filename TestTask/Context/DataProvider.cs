﻿using Microsoft.AspNetCore.Mvc;
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
                    EventDate = (DateTime)graftModel.EventDate,
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

        public async Task<DataProviderResult> DeleteGraft(GraftViewModel graftModel)
        {
            try
            {
                var graft = await GetGraft(graftModel.Id);
                if (graft == null) return new DataProviderResult { Succeeded = false, Errors = new List<string>() { "Прививка не найдена" } };
                if (graft.PatientId != graftModel.PatientId) return new DataProviderResult { Succeeded = false, Errors = new List<string>() { "Попытка удаления прививки у другого пользователя" } };
                context.Grafts.Remove(graft);
                await context.SaveChangesAsync();
                return new DataProviderResult { Succeeded = true };
            }
            catch { }
            return new DataProviderResult { Succeeded = false, Errors = new List<string>() { "Ошибка при удалении прививки" } };
        }

        public async Task<DataProviderResult> DeletePatient(PatientViewModel patientModel)
        {
            try
            {
                var patient = await GetPatient(patientModel.Id);
                if (patient == null) return new DataProviderResult { Succeeded = false, Errors = new List<string>() { "Пациент не найден" } };
                context.Patients.Remove(patient);
                await context.SaveChangesAsync();
                return new DataProviderResult { Succeeded = true };
            }
            catch { }
            return new DataProviderResult { Succeeded = false, Errors = new List<string>() { "Ошибка при удалении прививки" } };
        }

        public async Task<DataProviderResult> EditGraft(GraftViewModel graftModel)
        {
            try
            {
                var graft = await GetGraft(graftModel.Id);
                if (graft == null) return new DataProviderResult { Succeeded = false, Errors = new List<string>() { "Прививка не найдена" } };

                graftModel.CopyPropertyValuesTo(graft);
                await context.SaveChangesAsync();
                return new DataProviderResult { Succeeded = true };
            }
            catch { }
            return new DataProviderResult { Succeeded = false, Errors = new List<string>() { "Ошибка при редактировании прививки" } };
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

        public async Task<Graft> GetGraft(int? draftId)
        {
            if (!draftId.HasValue) return null;
            return await context.Grafts.Where(x => x.Id == draftId).FirstOrDefaultAsync();
        }

        public async Task<Patient> GetPatient(int? patientId)
        {
            if (!patientId.HasValue) return null;
            return await context.Patients.Where(x => x.Id == patientId).FirstOrDefaultAsync();
        }

        public async Task<Patient> GetPatientWithGrafts(int? patientId)
        {
            if (!patientId.HasValue) return null;
            return await context.Patients.Where(x => x.Id == patientId).Include(x => x.Grafts).FirstOrDefaultAsync();
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
