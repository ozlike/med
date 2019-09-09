using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.ViewModels;

namespace TestTask.Context
{
    public interface IGraftProvider
    {
        Task<DataProviderResult> AddGraft(GraftViewModel graftModel);
        Task<DataProviderResult> EditGraft(GraftViewModel graftModel);
        Task<DataProviderResult> DeleteGraft(GraftViewModel graftModel);
        Task<Graft> GetGraft(int? draftId);
        Task<Patient> GetPatient(int? patientId);
    }
}
