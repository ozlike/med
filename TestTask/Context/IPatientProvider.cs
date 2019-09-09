using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.ViewModels;

namespace TestTask.Context
{
    public interface IPatientProvider
    {
        Task<DataProviderResult> AddPatient(PatientViewModel patientModel);
        Task<DataProviderResult> EditPatient(PatientViewModel patientModel);
        Task<DataProviderResult> DeletePatient(PatientViewModel patientModel);
        Task<ICollection<Patient>> GetAllPatients();
        Task<Patient> GetPatient(int? patientId);
        Task<Patient> GetPatientWithGrafts(int? patientId);
        
    }
}
