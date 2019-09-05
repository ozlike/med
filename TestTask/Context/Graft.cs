using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Context
{
    public class Graft
    {
        public int Id { get; set; }
        public string Drug { get; set; }
        public bool Consent { get; set; }
        public DateTime EventDate { get; set; }

        public Patient Patient { get; set; }
        public int PatientId { get; set; }
    }
}
