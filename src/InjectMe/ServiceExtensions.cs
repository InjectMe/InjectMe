using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace InjectMe
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for getting service identities based on types and names.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Gets a <see cref="ServiceIdentity"/> for a type with the specified <paramref name="referenceName"/>.
        /// </summary>
        /// <param name="serviceType">
        /// A type to get the identity for.
        /// </param>
        /// <param name="referenceName">
        /// The name of an argument/variable/type to use as a reference when creating the identity.
        /// </param>
        /// <returns>
        /// A <see cref="ServiceIdentity"/> for the type and reference name, if the name matches the type; otherwise null.
        /// </returns>
        /// <remarks>
        /// Generic types are not valid if they are partially open. Only fully opened or fully closed types are allowed.
        /// </remarks>
        /// <example>
        /// <list type="table">
        /// <listheader>
        /// <term>Service type</term>
        /// <term>Reference name</term>
        /// <term>Service identity</term>
        /// </listheader>
        /// <item>
        /// <term>IFoo</term>
        /// <term>Foo</term>
        /// <term>IFoo, null</term>
        /// </item>
        /// <item>
        /// <term>IFoo</term>
        /// <term>SpecialFoo</term>
        /// <term>IFoo, "Special"</term>
        /// </item>
        /// <item>
        /// <term>IFoo</term>
        /// <term>Bar</term>
        /// <term>null</term>
        /// </item>
        /// <item>
        /// <term>IFoo&lt;int&gt;</term>
        /// <term>Foo</term>
        /// <term>IFoo&lt;int&gt;, null</term>
        /// </item>
        /// <item>
        /// <term>IFoo&lt;&gt;</term>
        /// <term>Foo</term>
        /// <term>IFoo&lt;&gt;, null</term>
        /// </item>
        /// </list>
        /// </example>
        public static ServiceIdentity TryGetServiceIdentity(this Type serviceType, string referenceName)
        {
            var serviceTypeInfo = serviceType.GetTypeInfo();

            return TryGetServiceIdentity(serviceType, serviceTypeInfo, referenceName);
        }

        /// <summary>
        /// Gets a <see cref="ServiceIdentity"/> for a type with the specified <paramref name="referenceName"/>.
        /// </summary>
        /// <param name="serviceTypeInfo">
        /// A type info to get the identity for.
        /// </param>
        /// <param name="referenceName">
        /// The name of an argument/variable/type to use as a reference when creating the identity.
        /// </param>
        /// <returns>
        /// A <see cref="ServiceIdentity"/> for the type and reference name, if the name matches the type; otherwise null.
        /// </returns>
        /// <remarks>
        /// Generic types are not valid if they are partially open. Only fully opened or fully closed types are allowed.
        /// </remarks>
        /// <example>
        /// <list type="table">
        /// <listheader>
        /// <term>Service type</term>
        /// <term>Reference name</term>
        /// <term>Service identity</term>
        /// </listheader>
        /// <item>
        /// <term>IFoo</term>
        /// <term>Foo</term>
        /// <term>IFoo, null</term>
        /// </item>
        /// <item>
        /// <term>IFoo</term>
        /// <term>SpecialFoo</term>
        /// <term>IFoo, "Special"</term>
        /// </item>
        /// <item>
        /// <term>IFoo</term>
        /// <term>Bar</term>
        /// <term>null</term>
        /// </item>
        /// <item>
        /// <term>IFoo&lt;int&gt;</term>
        /// <term>Foo</term>
        /// <term>IFoo&lt;int&gt;, null</term>
        /// </item>
        /// <item>
        /// <term>IFoo&lt;&gt;</term>
        /// <term>Foo</term>
        /// <term>IFoo&lt;&gt;, null</term>
        /// </item>
        /// </list>
        /// </example>
        public static ServiceIdentity TryGetServiceIdentity(this TypeInfo serviceTypeInfo, string referenceName)
        {
            var serviceType = serviceTypeInfo.AsType();

            return TryGetServiceIdentity(serviceType, serviceTypeInfo, referenceName);
        }

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