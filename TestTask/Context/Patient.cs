using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Context
{
    public class Patient
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public string Sex { get; set; }
        public string SNILS { get; set; }

        public ICollection<Graft> Grafts { get; set; }
    }
}
