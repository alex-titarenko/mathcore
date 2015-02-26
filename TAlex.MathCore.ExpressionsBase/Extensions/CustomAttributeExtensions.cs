using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;


namespace TAlex.MathCore.ExpressionEvaluation.Extensions
{
    internal static class CustomAttributeExtensions
    {
        public static T GetCustomAttribute<T>(this MemberInfo element) where T : Attribute
        {
            Attribute[] customAttributes = Attribute.GetCustomAttributes(element, typeof(T), true);
            if (customAttributes == null || customAttributes.Length == 0)
            {
                return null;
            }
            if (customAttributes.Length != 1)
            {
                throw new AmbiguousMatchException();
            }
            return (T)customAttributes[0];
        }


        public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element) where T : Attribute
        {
            return Attribute.GetCustomAttributes(element, typeof(T), true).Cast<T>();
        }
    }
}
