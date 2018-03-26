using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;


namespace TAlex.MathCore.ExpressionEvaluation.Extensions
{
    internal static class CustomAttributeExtensions
    {
        public static T GetCustomAttribute<T>(this Type element) where T : Attribute
        {
            return GetCustomAttribute<T>((MemberInfo)element.GetTypeInfo());
        }

        public static T GetCustomAttribute<T>(this MemberInfo element) where T : Attribute
        {
            Attribute[] customAttributes = element.GetCustomAttributes(typeof(T), true).Cast<T>().ToArray();
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

        public static IEnumerable<T> GetCustomAttributes<T>(this Type element) where T : Attribute
        {
            return GetCustomAttributes<T>((MemberInfo)element.GetTypeInfo());
        }

        public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element) where T : Attribute
        {
            return element.GetCustomAttributes(typeof(T), true).Cast<T>();
        }
    }
}
