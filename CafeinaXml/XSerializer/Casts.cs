using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeinaXml.XReflection;
using System.Reflection;
using System.Globalization;

namespace CafeinaXml.XSerializer
{
    internal class Casts
    {
        public static Object ToSave(Object obj, ReflectedProperty reflectedProperty)
        {
            PropertyInfo propertyInfo = reflectedProperty.PropertyInfo;


            if (propertyInfo.PropertyType.IsEnum)
            {
                return (int)propertyInfo.GetValue(obj, null);
            }

            if (propertyInfo.PropertyType == typeof(double))
            {
                return propertyInfo.GetValue(obj, null).ToString().Replace('.', ',');
            }

            return propertyInfo.GetValue(obj, null);
        }

        public static Object ToLoad(Object obj, Type propType)
        {
            if (propType == typeof(string))
                return obj.ToString();

            if (propType == typeof(int))
                return int.Parse(obj.ToString(), CultureInfo.CurrentCulture);

            if (propType == typeof(double))
                return double.Parse(obj.ToString(), CultureInfo.CurrentCulture);

            if (propType == typeof(float))
                return float.Parse(obj.ToString(), CultureInfo.CurrentCulture);

            if (propType == typeof(decimal))
                return decimal.Parse(obj.ToString(), CultureInfo.CurrentCulture);

            if (propType == typeof(bool))
                return bool.Parse(obj.ToString());

            if (propType.IsEnum)
                return Enum.Parse(propType, obj.ToString());


            return obj.ToString();
        }

        private Casts() 
        { }
    }
}
