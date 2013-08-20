using System;
using InjectMe.Caching;
using InjectMe.Construction;

namespace InjectMe.Activation
{
    public class FactoryActivator : IActivator
    {
        public IFactory Factory { get; private set; }
        public ServiceIdentity Identity { get; private set; }
        public IServiceScope Scope { get; set; }

        public FactoryActivator(
            ServiceIdentity identity,
            IFactory factory,
            IServiceScope scope)
        {
            if (identity == null)
                throw new ArgumentNullException("identity");

            if (factory == null)
                throw new ArgumentNullException("factory");

            if (scope == null)
                throw new ArgumentNullException("scope");

            Factory = factory;
            Identity = identity;
            Scope = scope;
        }

        public object ActivateService(IActivationContext context)
        {
            var cache = Scope.GetCache(context);
            var service = cache.Get(Identity, () => Factory.CreateService(context));

            return service;
        }
    }
}