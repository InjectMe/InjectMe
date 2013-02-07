using System.Reflection;
using System.Linq;

namespace Domo.DI.Registration.Conventions
{
    public class BasicScanConvention : IScanConvention
    {
        public void ProcessType(IContainerConfiguration container, TypeInfo type)
        {
            if (type.IsAbstract || type.IsInterface || type.IsGenericTypeDefinition || type.IsNested)
                return;

            if (type.Namespace != null &&
                type.Namespace.StartsWith("System"))
                return;

            ProcessConcreteType(container, type);
        }

        private static void ProcessConcreteType(IContainerConfiguration container, TypeInfo concreteType)
        {
            var serviceTypeName = "I" + concreteType.Name;
            var serviceType = concreteType.ImplementedInterfaces.FirstOrDefault(i => i.Name == serviceTypeName);

            if (serviceType != null)
            {
                var identity = new ServiceIdentity(serviceType);

                container.
                    Register(identity).
                    Using(concreteType);
            }
        }
    }
}