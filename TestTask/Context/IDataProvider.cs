using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.ViewModels;

namespace TestTask.Context
{
    public interface IDataProvider
    {
        DataProviderResult AddPatient(PatientViewModel patientModel);
        DataProviderResult EditPatient(PatientViewModel patientModel);
        ICollection<Patient> GetAllPatients();
        Patient GetPatient(int? id);
        Patient GetPatientWithGrafts(int? id);



    }
}
