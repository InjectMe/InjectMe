using System;
using System.Reflection;
using System.Web.Http;
using InjectMe.Registration;

namespace InjectMe.WebApi
{
    public class ApiControllerScanConvention : IScanConvention
    {
        private static readonly Type ApiControllerType;

        static ApiControllerScanConvention()
        {
            ApiControllerType = typeof(ApiController);
        }

        public void ProcessType(IContainerConfiguration container, TypeInfo type)
        {
            if (type.IsAbstract || type.IsInterface || type.IsGenericTypeDefinition || type.IsNested)
                return;

            ProcessConcreteType(container, type);
        }

        private static void ProcessConcreteType(IContainerConfiguration container, TypeInfo concreteType)
        {
            if (ApiControllerType.IsAssignableFrom(concreteType))
            {
                var serviceType = concreteType.AsType();

                container.
                    Register(serviceType).
                    AsTransient().
                    UsingConcreteType(concreteType);
            }
        }
    }
}
