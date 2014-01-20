using System;
using InjectMe.Caching;

namespace InjectMe.Registration
{
    /// <summary>
    /// The container configuration is used to register services in the container.
    /// </summary>
    public interface IContainerConfiguration
    {
        /// <summary>
        /// Register an activator configuration.
        /// </summary>
        /// <param name="activatorConfiguration"></param>
        /// <returns></returns>
        IContainerConfiguration Register(IActivatorConfiguration activatorConfiguration);
        IContainerConfiguration Register(Type serviceType, Action<IFluentConfiguration> action);

        IContainerConfiguration Register<TService>(Action<IFluentConfiguration<TService>> action)
            where TService : class;

        IFluentConfiguration Register(ServiceIdentity identity);
        IFluentConfiguration Register(Type serviceType, string serviceName = null);

        IFluentConfiguration<TService> Register<TService>(string serviceName = null)
            where TService : class;

        IContainerConfiguration RegisterInstance(Type serviceType, object instance, string serviceName = null);

        IContainerConfiguration RegisterInstance<TService>(TService instance, string serviceName = null)
            where TService : class;

        IContainerConfiguration RegisterScoped<TService>(IServiceScope scope, string serviceName = null)
            where TService : class;

        IContainerConfiguration RegisterScoped<TService>(IServiceScope scope, Func<TService> factory, string serviceName = null)
            where TService : class;

        IContainerConfiguration RegisterScoped<TService, TConcrete>(IServiceScope scope, string serviceName = null)
            where TConcrete : TService
            where TService : class;

        IContainerConfiguration RegisterSingleton<TService>(string serviceName = null)
            where TService : class;

        IContainerConfiguration RegisterSingleton<TService>(Func<TService> factory, string serviceName = null)
            where TService : class;

        IContainerConfiguration RegisterSingleton<TService, TConcrete>(string serviceName = null)
            where TConcrete : TService
            where TService : class;

        IContainerConfiguration RegisterTransient<TService>(string serviceName = null)
            where TService : class;

        IContainerConfiguration RegisterTransient<TService>(Func<TService> factory, string serviceName = null)
            where TService : class;

        IContainerConfiguration RegisterTransient<TService, TConcrete>(string serviceName = null)
            where TConcrete : TService
            where TService : class;

        IContainerConfiguration Scan(Action<IAssemblyScanner> scanner);
    }
}