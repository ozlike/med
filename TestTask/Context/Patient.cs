using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Context
{
    public class Patient
    {
        public int Id { get; set; }
        [MaxLength(70)]
        public string Surname { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public SexType Sex { get; set; }
        public string SNILS { get; set; }

        public ICollection<Graft> Grafts { get; set; }
    }
    
    public enum SexType
    {
        Male = 0,
        Female = 1,
    }
}
