using System;
using InjectMe.Activation;
using InjectMe.Caching;
using InjectMe.Construction;
using InjectMe.Extensions;

namespace InjectMe.Registration
{
    public class ScopedActivatorConfiguration<TService> :
        IScopedActivatorConfiguration<TService>,
        IScopedActivatorConfiguration
    {
        public ServiceIdentity Identity { get; private set; }
        public Type ConcreteType { get; set; }
        public IFactory Factory { get; set; }
        public IServiceScope ServiceScope { get; set; }

        public ScopedActivatorConfiguration(Type serviceType, IServiceScope serviceScope)
        {
            Identity = new ServiceIdentity(serviceType);
            ServiceScope = serviceScope;
        }

        public ScopedActivatorConfiguration(ServiceIdentity identity, IServiceScope serviceScope)
        {
            Identity = identity;
            ServiceScope = serviceScope;
        }

        IScopedActivatorConfiguration IScopedActivatorConfiguration.UsingConcreteType(Type concreteType)
        {
            if (Factory != null)
                throw new InvalidOperationException("A concrete type can't be specified when a factory has already been specified.");

            UsingConcreteType(concreteType);

            return this;
        }

        IScopedActivatorConfiguration IScopedActivatorConfiguration.UsingConcreteType<TConcrete>()
        {
            var concreteType = typeof(TConcrete);

            UsingConcreteType(concreteType);

            return this;
        }

        IScopedActivatorConfiguration IScopedActivatorConfiguration.UsingFactory(Func<object> factory)
        {
            var delegateFactory = new DelegateFactory(factory);

            UsingFactory(delegateFactory);

            return this;
        }

        IScopedActivatorConfiguration IScopedActivatorConfiguration.UsingFactory(Func<IActivationContext, object> factory)
        {
            var delegateFactory = new DelegateFactory(factory);

            UsingFactory(delegateFactory);

            return this;
        }

        IScopedActivatorConfiguration IScopedActivatorConfiguration.UsingFactory(IFactory factory)
        {
            UsingFactory(factory);

            return this;
        }

        public IScopedActivatorConfiguration<TService> UsingConcreteType(Type concreteType)
        {
            ConcreteType = concreteType;

            return this;
        }

        public IScopedActivatorConfiguration<TService> UsingConcreteType<TConcrete>() where TConcrete : TService
        {
            var concreteType = typeof(TConcrete);

            return UsingConcreteType(concreteType);
        }

        public IScopedActivatorConfiguration<TService> UsingFactory(Func<TService> factory)
        {
            var delegateFactory = new DelegateFactory<TService>(factory);

            UsingFactory(delegateFactory);

            return this;
        }

        public IScopedActivatorConfiguration<TService> UsingFactory(Func<IActivationContext, TService> factory)
        {
            var delegateFactory = new DelegateFactory<TService>(factory);

            UsingFactory(delegateFactory);

            return this;
        }

        public IScopedActivatorConfiguration<TService> UsingFactory(IFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            Factory = factory;

            return this;
        }

        public IActivator GetActivator(IContainer container)
        {
            return Identity.ServiceType.IsGenericTypeDefinition()
                ? GetUnboundActivator()
                : GetScopedActivator();
        }

        private IActivator GetUnboundActivator()
        {
            return new UnboundActivator(Identity, ServiceScope, ConcreteType ?? Identity.ServiceType);
        }

        private IActivator GetScopedActivator()
        {
            var factory = Factory ?? new ConstructionFactory(ConcreteType ?? Identity.ServiceType);
            var activator = new ScopedActivator(Identity, factory, ServiceScope);

            return activator;
        }
    }
}