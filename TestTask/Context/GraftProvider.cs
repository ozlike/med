using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Extensions;
using TestTask.ViewModels;

namespace TestTask.Context
{
    public class GraftProvider : IGraftProvider
    {
        MedContext context;
        public GraftProvider(MedContext context)
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
    }
}
