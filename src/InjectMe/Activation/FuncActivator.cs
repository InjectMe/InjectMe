using System;
using InjectMe.Registration;

namespace InjectMe.Activation
{
    public class FuncActivator : IActivator
    {
        private readonly Func<IActivationContext, object> _delegate;

        public FuncActivator(ServiceIdentity identity, IActivator realActivator)
        {
            Identity = identity;
            _delegate = realActivator.GetTypedFuncDelegate();
        }

        public ServiceIdentity Identity { get; private set; }

        public object ActivateService(IActivationContext context)
        {
            return _delegate(context);
        }
    }
}