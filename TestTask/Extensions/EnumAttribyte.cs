using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask.Extensions
{
    public static class EnumAttribyte
    {
        public static string GetDisplayNameAttribute(this Enum enumValue)
        {
            var attr = enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .FirstOrDefault();
            if (attr == null) return "";
            return attr.GetCustomAttribute<DisplayNameAttribute>().DisplayName;
        }
    }
}