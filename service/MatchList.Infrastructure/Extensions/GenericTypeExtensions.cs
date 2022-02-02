using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace MatchList.Infrastructure.Extensions
{
    public static class GenericTypeExtensions
    {
        public static string GetGenericTypeName(this Type type)
        {
            string typeName;
            if (type.IsGenericType)
            {
                var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
                typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
            }
            else
                typeName = type.Name;

            return typeName;
        }

        public static string GetGenericTypeName(this object @object)
        {
            return @object.GetType().GetGenericTypeName();
        }
        
        public static DateTime? GetValueAsDateTime(this XAttribute attribute, IFormatProvider formatProvider)
        {

            if (String.IsNullOrEmpty(attribute.Value) == false)
            {
                DateTime dateTime;
                if (DateTime.TryParse(attribute.Value, formatProvider, DateTimeStyles.None, out dateTime))
                {
                    return dateTime;
                }
            }

            return null;
        }
    }
}
