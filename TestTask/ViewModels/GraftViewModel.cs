using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.ViewModels
{
    public class GraftViewModel
    {
        [DisplayName("Препарат")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Препарат не выбран")]
        public string Drug { get; set; }

        [DisplayName("Согласие пациента")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле \"{0}\" не должно быть пустым")]
        public bool Consent { get; set; }

        [DisplayName("Дата проведения")]
        [PastDateValidation(150)]
        public DateTime EventDate { get; set; }

        public string PatientFullName { get; set; }
        public int PatientId { get; set; }
    }
}
