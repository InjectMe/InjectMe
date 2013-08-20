using System;

namespace InjectMe.Registration
{
    public interface IContainerConfiguration
    {
        IContainerConfiguration Register(IActivatorConfiguration activatorConfiguration);
        IContainerConfiguration Register(Type serviceType, Action<IFluentConfiguration> action);
        IContainerConfiguration Register<TService>(Action<IFluentConfiguration<TService>> action) where TService : class;
        IFluentConfiguration Register(ServiceIdentity identity);
        IFluentConfiguration Register(Type serviceType, string serviceName = null);
        IFluentConfiguration<TService> Register<TService>(string serviceName = null) where TService : class;

        IContainerConfiguration Scan(Action<IAssemblyScanner> scanner);

        void ApplyRegistrations(IContainer container);
    }
}