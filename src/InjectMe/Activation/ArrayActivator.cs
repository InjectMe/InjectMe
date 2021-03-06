using System;
using InjectMe.Registration;

namespace InjectMe.Activation
{
    public class ArrayActivator : IActivator
    {
        private readonly Func<IActivationContext, object> _delegate;

        public ArrayActivator(ServiceIdentity identity, Type itemServiceType, IActivator[] activators)
        {
            Identity = identity;
            _delegate = activators.GetTypedArrayDelegate(itemServiceType);
        }

        public ServiceIdentity Identity { get; private set; }

        public object ActivateService(IActivationContext context)
        {
            return _delegate(context);
        }
    }
}