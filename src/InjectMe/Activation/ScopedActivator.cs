using System;
using InjectMe.Caching;
using InjectMe.Construction;

namespace InjectMe.Activation
{
    /// <summary>
    /// Represents an activator that uses a factory and a scope to activate and cache an instance.
    /// </summary>
    /// <remarks>
    /// The scoped activator is used when InjectMe constructs a service.
    /// </remarks>
    /// <remarks>
    /// It contains an <see cref="IFactory"/>, which is responsible for constructing the service, and an <see cref="IServiceScope"/> which is responsible for caching the service.
    /// InjectMe comes with a <see cref="ConstructionFactory"/> which will create the service by invoking the most relevant constructor, and a <see cref="DelegateFactory"/> which leaves the construction to a delegate method.
    /// </remarks>
    public class ScopedActivator : IActivator
    {
        /// <summary>Gets the <see cref="IFactory"/> responsible for the construction of the service.</summary>
        public IFactory Factory { get; private set; }

        /// <summary>Gets the <see cref="ServiceIdentity"/> of the service to construct.</summary>
        public ServiceIdentity Identity { get; private set; }

        /// <summary>Gets the <see cref="IServiceScope"/> of the constructed service.</summary>
        public IServiceScope Scope { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ScopedActivator"/> class.</summary>
        /// <param name="identity">The identity of the service to activate.</param>
        /// <param name="factory">The factory responsible for the construction of the service.</param>
        /// <param name="scope">The scope of the constructed service.</param>
        public ScopedActivator(ServiceIdentity identity, IFactory factory, IServiceScope scope)
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