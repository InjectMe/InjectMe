using System;
using System.Reflection;
using Domo.DI.Activation;

namespace Domo.DI.Registration
{
    public class InstanceActivatorRegistration<TService> :
        IInstanceActivatorRegistration<TService>,
        IInstanceActivatorRegistration,
        IActivatorConfiguration
        where TService : class
    {
        public ServiceIdentity Identity { get; private set; }
        public TService Instance { get; private set; }

        ServiceIdentity IActivatorRegistration.Identity
        {
            get { return Identity; }
        }

        object IInstanceActivatorRegistration.Instance
        {
            get { return Instance; }
        }

        public InstanceActivatorRegistration(ServiceIdentity identity, TService instance)
        {
            if (identity == null)
                throw new ArgumentNullException("identity");

            if (instance == null)
                throw new ArgumentNullException("instance");

            if (!IsValidInstance(identity, instance))
                throw new ArgumentException("instance", "TODO");

            Identity = identity;
            Instance = instance;
        }

        public InstanceActivatorRegistration(IFluentRegistration fluentRegistration, TService instance)
        {
            if (fluentRegistration == null)
                throw new ArgumentNullException("fluentRegistration");

            if (instance == null)
                throw new ArgumentNullException("instance");

            if (!IsValidInstance(fluentRegistration.Identity, instance))
                throw new ArgumentException("instance", "TODO");

            Identity = fluentRegistration.Identity;
            Instance = instance;

            fluentRegistration.Using(this);
        }

        public InstanceActivatorRegistration(IFluentRegistration<TService> fluentRegistration, TService instance)
        {
            if (fluentRegistration == null)
                throw new ArgumentNullException("fluentRegistration");

            if (instance == null)
                throw new ArgumentNullException("instance");

            if (!IsValidInstance(fluentRegistration.Identity, instance))
                throw new ArgumentException("instance", "TODO");

            Identity = fluentRegistration.Identity;
            Instance = instance;

            fluentRegistration.Using(this);
        }

        public IActivator GetService(IContainer container)
        {
            return new InstanceActivator(Identity, Instance);
        }

        private static bool IsValidInstance(ServiceIdentity identity, TService instance)
        {
            var serviceTypeInfo = identity.ServiceType.GetTypeInfo();
            var instanceTypeInfo = instance.GetType().GetTypeInfo();

            return serviceTypeInfo.IsAssignableFrom(instanceTypeInfo);
        }
    }
}