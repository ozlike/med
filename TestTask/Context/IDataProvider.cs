using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.ViewModels;

namespace TestTask.Context
{
    public interface IDataProvider
    {
        Task<DataProviderResult> AddPatient(PatientViewModel patientModel);
        Task<DataProviderResult> EditPatient(PatientViewModel patientModel);
        Task<ICollection<Patient>> GetAllPatients();
        Task<Patient> GetPatient(int? id);
        Task<Patient> GetPatientWithGrafts(int? id);


        Task<DataProviderResult> AddGraft(GraftViewModel graftModel);


    }
}
