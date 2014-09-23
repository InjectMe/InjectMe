using System;
using InjectMe.Caching;

namespace InjectMe.Registration
{
    public class FluentConfiguration : IFluentConfiguration
    {
        protected static readonly IServiceScope TransientScope = new TransientScope();
        protected static readonly IServiceScope SingletonScope = new SingletonScope();

        private readonly IContainerConfiguration _containerConfiguration;
        private IActivatorConfiguration _activatorConfiguration;

        public ServiceIdentity Identity { get; private set; }

        public FluentConfiguration(ServiceIdentity identity, IContainerConfiguration containerConfiguration)
        {
            if (identity == null)
                throw new ArgumentNullException("identity");

            if (containerConfiguration == null)
                throw new ArgumentNullException("containerConfiguration");

            _containerConfiguration = containerConfiguration;

            Identity = identity;
        }

        public IActivatorConfiguration Configuration
        {
            get { return _activatorConfiguration; }
            set
            {
                if (_activatorConfiguration != null)
                    throw new ActivatorConfigurationAlreadyRegisteredException(Identity);

                _activatorConfiguration = value;
                _containerConfiguration.Register(value);
            }
        }

        public IScopedActivatorConfiguration AsSingleton()
        {
            return InScope(SingletonScope);
        }

        public IScopedActivatorConfiguration AsTransient()
        {
            return InScope(TransientScope);
        }

        public IScopedActivatorConfiguration InScope(IServiceScope serviceScope)
        {
            var configuration = new ScopedActivatorConfiguration<object>(Identity, serviceScope);

            Configuration = configuration;

            return configuration;
        }

        public IInstanceActivatorConfiguration UsingInstance(object instance)
        {
            var configuration = new InstanceActivatorConfiguration<object>(Identity, instance);

            Configuration = configuration;

            return configuration;
        }
    }

    public class FluentConfiguration<TService> :
        FluentConfiguration,
        IFluentConfiguration<TService>
        where TService : class
    {
        public FluentConfiguration(string serviceName, IContainerConfiguration containerConfiguration)
            : base(new ServiceIdentity(typeof(TService), serviceName), containerConfiguration)
        {
        }

        public new IScopedActivatorConfiguration<TService> AsSingleton()
        {
            return InScope(SingletonScope);
        }

        public new IScopedActivatorConfiguration<TService> AsTransient()
        {
            return InScope(TransientScope);
        }

        public new IScopedActivatorConfiguration<TService> InScope(IServiceScope serviceScope)
        {
            var configuration = new ScopedActivatorConfiguration<TService>(Identity, serviceScope);

            Configuration = configuration;

            return configuration;
        }

        public IInstanceActivatorConfiguration<TService> UsingInstance(TService instance)
        {
            var configuration = new InstanceActivatorConfiguration<TService>(Identity, instance);

            Configuration = configuration;

            return configuration;
        }
    }
}