using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace InjectMe
{
    public static class ServiceExtensions
    {
        public static ServiceIdentity TryGetServiceIdentity(this Type serviceType, string referenceName)
        {
            var serviceTypeInfo = serviceType.GetTypeInfo();

            return TryGetServiceIdentity(serviceType, serviceTypeInfo, referenceName);
        }

        public static ServiceIdentity TryGetServiceIdentity(this TypeInfo serviceTypeInfo, string referenceName)
        {
            var serviceType = serviceTypeInfo.AsType();

            return TryGetServiceIdentity(serviceType, serviceTypeInfo, referenceName);
        }

        // IFoo, "SpecialFoo"      -> IFoo, "Special"
        // IFoo, "Foo"             -> IFoo, null
        // IFoo<int>, "SpecialFoo" -> IFoo<int>, "Special"
        // IFoo<int>, "Foo"        -> IFoo<int>, null
        // IFoo<>, "Foo<>"         -> IFoo<>, null
        // Foo, "SpecialFoo"       -> Foo, "Special"
        // Foo, "Foo"              -> Foo, null
        // IFoo, "Something"       -> null
        private static ServiceIdentity TryGetServiceIdentity(Type serviceType, TypeInfo serviceTypeInfo, string referenceName)
        {
            // Check if the service is a generic type definition
            if (IsClosedGenericTypeDefinition(serviceTypeInfo))
            {
                var allParametersAreGeneric = serviceTypeInfo.GenericTypeArguments.All(a => a.IsGenericParameter);
                if (allParametersAreGeneric == false)
                {
                    // Generic services are only valid if all type arguments are generic
                    return null;
                }

                serviceType = serviceType.GetGenericTypeDefinition();
            }

            // Strip away potential leading I and trailing generic constructs.
            var serviceTypeName = Regex.Replace(serviceType.Name, @"^I|`\d+$", string.Empty);
            referenceName = Regex.Replace(referenceName, @"`\d+$", string.Empty);

            if (referenceName.EndsWith(serviceTypeName, StringComparison.OrdinalIgnoreCase))
            {
                var serviceNameLength = referenceName.Length - serviceTypeName.Length;
                var serviceName = (serviceNameLength > 0)
                    ? referenceName.Substring(0, serviceNameLength)
                    : null;

                return new ServiceIdentity(serviceType, serviceName);
            }

            return null;
        }

        private static bool IsClosedGenericTypeDefinition(TypeInfo serviceType)
        {
            return
                serviceType.IsGenericType &&
                serviceType.FullName == null;
        }
    }
}