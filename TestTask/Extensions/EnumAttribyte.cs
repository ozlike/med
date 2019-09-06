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
        public static string GetDisplayNameAttribute(Type t)
        {
            var attr = SexType.Female.GetType()
                        .GetMember(SexType.Female.ToString())
                        .FirstOrDefault();
            if (attr == null) return "";

            var name = attr.GetCustomAttribute<DisplayNameAttribute>().DisplayName;



            return "";
        }
    }
}


//public static class Extensions
//{
//    /// <summary>
//    ///     A generic extension method that aids in reflecting 
//    ///     and retrieving any attribute that is applied to an `Enum`.
//    /// </summary>
//    public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
//            where TAttribute : Attribute
//    {
//        return enumValue.GetType()
//                        .GetMember(enumValue.ToString())
//                        .First()
//                        .GetCustomAttribute<TAttribute>();
//    }
//}
