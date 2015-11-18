using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TaskShelf_www.Helpers
{
    public static class EnumHelper
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static string GetEnumDescription<T>(this T enumeratedType)
        {
            var description = enumeratedType.ToString();

            var enumType = typeof(T);
            // Can't use type constraints on value types, so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            var fieldInfo = enumeratedType.GetType().GetField(enumeratedType.ToString());

            if (fieldInfo != null)
            {
                var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    description = ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            return description;
        }      
    }
}