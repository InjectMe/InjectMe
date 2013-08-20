using System;

namespace InjectMe.Registration
{
    public static class InstanceActivatorRegistrationExtensions
    {
        public static IContainerConfiguration RegisterInstance(this IContainerConfiguration configuration, Type serviceType, object instance, string serviceName = null)
        {
            var identity = new ServiceIdentity(serviceType, serviceName);
            var activatorConfiguration = new InstanceActivatorRegistration<object>(identity, instance);

            return configuration.Register(activatorConfiguration);
        }

        public static IContainerConfiguration RegisterInstance<TService>(this IContainerConfiguration configuration, TService instance, string serviceName = null)
            where TService : class
        {
            var serviceType = typeof(TService);
            var identity = new ServiceIdentity(serviceType, serviceName);
            var activatorConfiguration = new InstanceActivatorRegistration<TService>(identity, instance);

            return configuration.Register(activatorConfiguration);
        }

        public static IInstanceActivatorRegistration UsingInstance(this IFluentConfiguration fluentRegistration, object instance)
        {
            var registration = new InstanceActivatorRegistration<object>(fluentRegistration.Identity, instance);

            fluentRegistration.Configuration = registration;

            return registration;
        }

        public static IInstanceActivatorRegistration<TService> UsingInstance<TService>(this IFluentConfiguration<TService> fluentConfiguration, TService instance)
            where TService : class
        {
            var registration = new InstanceActivatorRegistration<TService>(fluentConfiguration.Identity, instance);

            fluentConfiguration.Configuration = registration;

            return registration;
        }
    }
}