namespace InjectMe.Activation
{
    public class ServiceLoaderActivator<TService> : IActivator
        where TService : class
    {
        private readonly IServiceLoader<TService> _loader;

        public ServiceIdentity Identity { get; private set; }

        public ServiceLoaderActivator(IServiceLoader<TService> loader)
        {
            _loader = loader;
            Identity = new ServiceIdentity(typeof(TService));
        }

        public object ActivateService(IActivationContext context)
        {
            return _loader.LoadService();
        }
    }
}