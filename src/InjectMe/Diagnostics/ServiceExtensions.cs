using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace InjectMe.Diagnostics
{
    public static class ServiceExtensions
    {
        public static string ToDebugString(this Type type)
        {
            var result = type.FullName;
            var typeInfO = type.GetTypeInfo();

            if (typeInfO.IsGenericType)
            {
                var arguments = GetGenericArguments(typeInfO);

                result = string.Format(
                    "{0}<{1}>",
                    Regex.Replace(result, @"`.*$", string.Empty),
                    string.Join(", ", arguments));
            }

            return result;
        }

        private static IEnumerable<string> GetGenericArguments(TypeInfo typeInfo)
        {
            if (typeInfo.IsGenericTypeDefinition)
            {
                return typeInfo.
                    GenericTypeParameters.
                    Select(a => a.Name);
            }
            else
            {
                return typeInfo.
                    GenericTypeArguments.
                    Select(a => a.ToDebugString());
            }
        }
    }
}