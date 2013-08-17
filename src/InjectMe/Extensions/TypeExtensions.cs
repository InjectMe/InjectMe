using System;
using System.Reflection;
#if NETFX_CORE

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
#if NETFX_CORE
            return baseType.GetTypeInfo().IsAssignableFrom(instanceType.GetTypeInfo());
#else
            return baseType.IsAssignableFrom(instanceType);
#endif
        }
    }
}