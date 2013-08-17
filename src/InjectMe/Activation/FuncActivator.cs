using System;
using InjectMe.Registration;

namespace InjectMe.Activation
{
    public class FuncActivator : IActivator
    {
        private readonly Func<IInjectionContext, object> _delegate;

        public FuncActivator(ServiceIdentity identity, IActivator realActivator)
        {
            Identity = identity;
            _delegate = realActivator.GetTypedFuncDelegate();
        }

        public ServiceIdentity Identity { get; private set; }

        public object ActivateService(IInjectionContext context)
        {
            return _delegate(context);
        }
    }
}