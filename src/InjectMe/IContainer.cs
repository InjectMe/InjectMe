using System;
using System.Collections.Generic;
using InjectMe.Activation;
using InjectMe.Registration;

namespace InjectMe
{
    public interface IContainer
    {
        IServiceLocator ServiceLocator { get; }

        void Configure(Action<IContainerConfiguration> configure = null);
        void Register(IActivatorGroup activatorGroup);
        void Register(IActivator activator);
        object Resolve(ServiceIdentity identity);
        IEnumerable<object> ResolveAll(Type serviceType);

        IActivator GetActivator(ServiceIdentity identity);
        IActivator[] GetAllActivators(Type serviceType);
    }
}