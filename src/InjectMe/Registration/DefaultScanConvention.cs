using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using InjectMe.Caching;

namespace InjectMe.Registration
{
    public class DefaultScanConvention : IScanConvention
    {
        public bool UsePrefixResolution { get; private set; }

        public DefaultScanConvention(bool usePrefixResolution)
        {
            UsePrefixResolution = usePrefixResolution;
        }

        public void ProcessType(IContainerConfiguration container, TypeInfo type)
        {
            if (type.IsAbstract || type.IsInterface || type.IsNested)
                return;

            if (type.Namespace != null &&
                type.Namespace.StartsWith("System"))
                return;

            ProcessConcreteType(container, type);
        }

        private void ProcessConcreteType(IContainerConfiguration container, TypeInfo concreteType)
        {
            var identities = GetIdentities(concreteType);

            foreach (var identity in identities)
            {
                var serviceType = identity.ServiceType.GetTypeInfo();
                var scope =
                    concreteType.GetServiceScope() ??
                    serviceType.GetServiceScope() ??
                    ServiceScope.Default;

                container.
                    Register(identity).
                    InScope(scope).
                    UsingConcreteType(concreteType.AsType());
            }
        }

        private IEnumerable<ServiceIdentity> GetIdentities(TypeInfo concreteType)
        {
            if (UsePrefixResolution)
            {
                return GetPrefixedIdentities(concreteType);
            }
            else
            {
                var defaultIdentity = GetDefaultIdentity(concreteType);

                return defaultIdentity != null
                    ? new[] { defaultIdentity }
                    : new ServiceIdentity[0];
            }
        }

        private static IEnumerable<ServiceIdentity> GetPrefixedIdentities(TypeInfo concreteType)
        {
            return from serviceType in concreteType.ImplementedInterfaces
                   let identity = serviceType.TryGetServiceIdentity(concreteType.Name)
                   where identity != null
                   select identity;
        }

        private static ServiceIdentity GetDefaultIdentity(TypeInfo concreteType)
        {
            var serviceTypeName = "I" + concreteType.Name;
            var serviceType = concreteType.ImplementedInterfaces.FirstOrDefault(i => i.Name == serviceTypeName);

            if (serviceType != null)
            {
                return new ServiceIdentity(serviceType);
            }

            return null;
        }
    }
}