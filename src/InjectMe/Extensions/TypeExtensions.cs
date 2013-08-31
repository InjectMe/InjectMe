using System;

#if !NET45
using System.Reflection;
#endif

namespace InjectMe.Extensions
{
    public static class TypeExtensions
    {
        public static bool Implements<TInterface>(this Type instanceType)
        {
            var baseType = typeof(TInterface);

            return Implements(instanceType, baseType);
        }

        public static bool Implements(this Type instanceType, Type baseType)
        {
#if NET45
            return baseType.IsAssignableFrom(instanceType);
#else
            return baseType.GetTypeInfo().IsAssignableFrom(instanceType.GetTypeInfo());
#endif
        }
    }
}