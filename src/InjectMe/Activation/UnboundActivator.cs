using System;
using InjectMe.Caching;
using InjectMe.Construction;

namespace InjectMe.Activation
{
    public class UnboundActivator : IActivator, IUnboundActivator
    {
        public Type ConcreteType { get; private set; }
        public ServiceIdentity Identity { get; private set; }
        public IServiceScope Scope { get; private set; }

        public UnboundActivator(ServiceIdentity identity, IServiceScope scope, Type concreteType)
        {
            if (identity == null)
                throw new ArgumentNullException("identity");

            if (identity.ServiceType.IsGenericTypeDefinition == false)
                throw new ArgumentException("An unbound activator can only be created for generic type definitions.");

            if (scope == null)
                throw new ArgumentNullException("scope");

            if (concreteType == null)
                throw new ArgumentNullException("concreteType");

            ConcreteType = concreteType;
            Identity = identity;
            Scope = scope;
        }

        public object ActivateService(IActivationContext context)
        {
            throw new UnboundTypeActivationFailedException(ConcreteType);
        }

        public IActivator ConstructBoundActivator(Type[] genericArguments)
        {
            var boundServiceType = Identity.ServiceType.MakeGenericType(genericArguments);
            var boundConcreteType = ConcreteType.MakeGenericType(genericArguments);
            var boundIdentity = new ServiceIdentity(boundServiceType, Identity.ServiceName);
            var factory = new ConstructionFactory(boundConcreteType);

            return new ScopedActivator(boundIdentity, factory, Scope);
        }
    }
}