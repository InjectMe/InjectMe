using System;
using InjectMe.Registration;

namespace InjectMe.Activation
{
    public class LazyActivator : IActivator
    {
        private readonly Func<IActivationContext, object> _delegate;

        public LazyActivator(ServiceIdentity identity, IActivator realActivator)
        {
            Identity = identity;
            _delegate = realActivator.GetTypedLazyDelegate();
        }

        public ServiceIdentity Identity { get; private set; }

        public object ActivateService(IActivationContext context)
        {
            return _delegate(context);
        }
    }
}