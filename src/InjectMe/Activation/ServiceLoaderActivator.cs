using System;

namespace InjectMe.Activation
{
    public class ServiceLoaderActivator<TService> : IActivator
        where TService : class
    {
        private readonly Lazy<IServiceLoader<TService>> _lazyServiceLoader;

        public ServiceIdentity Identity { get; private set; }

        public ServiceLoaderActivator(Lazy<IServiceLoader<TService>> lazyServiceLoader)
        {
            _lazyServiceLoader = lazyServiceLoader;

            Identity = new ServiceIdentity(typeof(TService));
        }

        public object ActivateService(IActivationContext context)
        {
            return _lazyServiceLoader.Value.LoadService();
        }
    }
}