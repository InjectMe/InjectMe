using System;
using InjectMe.Activation;
using InjectMe.Construction;

namespace InjectMe.Registration
{
    public class ServiceLoaderActivatorConfiguration : IActivatorConfiguration
    {
        private readonly Type _serviceType;

        public ServiceLoaderActivatorConfiguration(Type serviceType)
        {
            _serviceType = serviceType;
        }

        public IActivator GetActivator(IContainer container)
        {
            var activatorType = typeof(ServiceLoaderActivator<>).MakeGenericType(_serviceType);
            var activator = (IActivator)activatorType.ConstructInstance(container);

            return activator;
        }
    }
}