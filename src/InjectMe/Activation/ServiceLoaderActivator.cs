using System;

namespace InjectMe.Activation
{
    public class ServiceLoaderActivator<TService> : IActivator
        where TService : class
    {
        private readonly Func<IActivationContext, IServiceLoader<TService>> _serviceLoaderFactory;

        public ServiceIdentity Identity { get; private set; }

        public ServiceLoaderActivator(IContainer container)
        {
            _serviceLoaderFactory = GetServiceLoaderFactory(container);

            Identity = new ServiceIdentity(typeof(TService));
        }

        public object ActivateService(IActivationContext context)
        {
            var serviceLoader = _serviceLoaderFactory(context);
            var service = serviceLoader.LoadService();

            return service;
        }

        private static Func<IActivationContext, IServiceLoader<TService>> GetServiceLoaderFactory(IContainer container)
        {
            var serviceLoaderType = typeof(IServiceLoader<TService>);
            var serviceLoaderIdentity = new ServiceIdentity(serviceLoaderType);
            var serviceLoaderActivator = container.GetActivator(serviceLoaderIdentity);

            return context => (IServiceLoader<TService>)serviceLoaderActivator.ActivateService(context);
        }
    }
}