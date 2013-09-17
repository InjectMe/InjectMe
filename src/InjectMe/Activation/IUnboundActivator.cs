using System;

namespace InjectMe.Activation
{
    public interface IUnboundActivator
    {
        IActivator ConstructBoundActivator(Type[] genericArguments);
    }
}