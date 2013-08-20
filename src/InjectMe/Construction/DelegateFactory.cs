using System;
using InjectMe.Activation;

namespace InjectMe.Construction
{
    public class DelegateFactory : IFactory
    {
        private readonly Func<IActivationContext, object> _factoryDelegate;

        public DelegateFactory(Func<object> factoryDelegate)
        {
            _factoryDelegate = context => factoryDelegate();
        }

        public DelegateFactory(Func<IActivationContext, object> factoryDelegate)
        {
            _factoryDelegate = factoryDelegate;
        }

        public object CreateService(IActivationContext context)
        {
            return _factoryDelegate(context);
        }
    }

    public class DelegateFactory<TService> : IFactory
    {
        private readonly Func<IActivationContext, TService> _factoryDelegate;

        public DelegateFactory(Func<TService> factoryDelegate)
        {
            _factoryDelegate = context => factoryDelegate();
        }

        public DelegateFactory(Func<IActivationContext, TService> factoryDelegate)
        {
            _factoryDelegate = factoryDelegate;
        }

        public object CreateService(IActivationContext context)
        {
            return _factoryDelegate(context);
        }
    }
}