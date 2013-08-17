using System;

namespace InjectMe.Activation
{
    public interface IActivatorGroup
    {
        Type ServiceType { get; }

        void Add(IActivator activator);
        IActivator GetActivator(ServiceIdentity identity);
        IActivator[] GetAllActivators();
    }
}