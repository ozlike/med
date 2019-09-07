using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using TestTask.Context;
using TestTask.Models;

namespace TestTask.ViewModels
{
    public class PatientViewModel
    {
        public int? Id { get; set; }

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
        [PastDateValidation(150, ErrorMessage = "Ошибка в дате рождения")]
        public DateTime Birthday { get; set; }

        [DisplayName("Пол")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Пол не выбран")]
        public SexType? Sex { get; set; }

        [DisplayName("Снилс")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле \"{0}\" не должно быть пустым")]
        [SnilsMask()]
        public string SNILS { get; set; }
    }


    [AttributeUsage(AttributeTargets.Property)]
    public class SnilsMask : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            ErrorMessage = "Снилс неверного формата";
            if (value == null) return false;
            var snils = value.ToString();
            if (!Regex.IsMatch(snils, @"\A[0-9 \-]+\z")) return false;
            snils = string.Join("", snils.Where(x => char.IsDigit(x)));
            if (snils.Length != 11) return false;
            int sum = 0;
            List<int> repeated = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                int num = (int)char.GetNumericValue(snils[i]);
                sum += num * (9 - i);
                repeated.Add(num);
                if (repeated.Count < 3) continue;
                if (repeated.All(x => repeated[0] == x)) return false;
                repeated.RemoveAt(0);
            }
            int checkSum = Convert.ToInt32("" + snils[9] + snils[10]);
            if (sum < 100) return sum == checkSum;
            if (sum == 100 || sum == 101) return checkSum == 0;
            return sum % 101 == checkSum || (sum % 101 == 100 && checkSum == 0);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PastDateValidation : ValidationAttribute
    {
        public int MaximumAge { get; private set; }

        public PastDateValidation(int MaximumAge)
        {
            this.MaximumAge = MaximumAge;
        }

        public override bool IsValid(object value)
        {
            ErrorMessage = "Ошибка в дате";
            if (value == null) return false;
            var date = Convert.ToDateTime(value);
            if (date > DateTime.Now) return false;
            if (date < DateTime.Now.AddYears(-MaximumAge)) return false;
            return true;
        }
    }
}
