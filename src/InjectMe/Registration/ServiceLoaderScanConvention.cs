using System;
using System.Reflection;
using InjectMe.Activation;

namespace InjectMe.Registration
{
    public class ServiceLoaderScanConvention : IScanConvention
    {
        private static readonly Type ServiceLoaderTypeDefinition = typeof(IServiceLoader<>);

        public void ProcessType(IContainerConfiguration container, TypeInfo type)
        {
            foreach (var serviceType in type.ImplementedInterfaces)
            {
                if (serviceType.IsConstructedGenericType)
                {
                    var typeDefinition = serviceType.GetGenericTypeDefinition();
                    if (typeDefinition == ServiceLoaderTypeDefinition)
                    {
                        container.
                            Register(serviceType).
                            AsTransient().
                            UsingConcreteType(type.AsType());

                        var dataType = serviceType.GenericTypeArguments[0];
                        var dataTypeConfiguration = new ServiceLoaderActivatorConfiguration(dataType);

                        container.Register(dataTypeConfiguration);
                    }
                }
            }
        }
    }
}