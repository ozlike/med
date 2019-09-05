using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TestTask.Context;

namespace TestTask.ViewModels
{
    public class CreatePatientViewModel
    {
        [DisplayName("Фамилия")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле \"{0}\" не должно быть пустым")]
        [StringLength(70, ErrorMessage = "{0} должна иметь от {2} до {1} символов", MinimumLength = 2)]
        public string Surname { get; set; }

        [DisplayName("Имя")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле \"{0}\" не должно быть пустым")]
        [StringLength(50, ErrorMessage = "{0} должно иметь от {2} до {1} символов", MinimumLength = 2)]
        public string Name { get; set; }

        [DisplayName("Отчество")]
        public string Patronymic { get; set; }

        [DisplayName("Дата рождения")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле \"{0}\" не должно быть пустым")]
        public DateTime Birthday { get; set; }

        [DisplayName("Пол")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле \"{0}\" не должно быть пустым")]
        public SexType Sex { get; set; }

        [DisplayName("Снилс")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле \"{0}\" не должно быть пустым")]
        //[RegularExpression("", ErrorMessage = "Неверный формат маски")]
        [SnilsMask()]
        public string SNILS { get; set; }
    }


    //[AttributeUsage(AttributeTargets.Field)]
    public class SnilsMask : ValidationAttribute
    {
        public override bool IsValid(object value)
        {



            this.ErrorMessage = "";

            
            return false;
        }
    }

}
