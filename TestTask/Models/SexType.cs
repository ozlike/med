using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public enum SexType
    {
        [DisplayName("Мужской")]
        Male = 0,
        [DisplayName("Женский")]
        Female = 1,
    }
}
