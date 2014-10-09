using System;
using System.Collections.Generic;
using InjectMe.Activation;
using InjectMe.Registration;

namespace InjectMe
{
    /// <summary>
    /// Responsible for keeping track of all registered services.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Gets a reference to a service locator for the container.
        /// </summary>
        IServiceLocator ServiceLocator { get; }

        /// <summary>
        /// Configure thes the container by adding new registrations.
        /// </summary>
        /// <param name="configure"></param>
        void Configure(Action<IContainerConfiguration> configure = null);

        /// <summary>
        /// Registers an activator group in the container.
        /// The activator group is registered using its <see cref="InjectMe.Activation.IActivatorGroup.ServiceType"/>.
        /// </summary>
        /// <remarks>
        /// A <see cref="InjectMe.Activation.DefaultActivatorGroup"/> is automatically created when an activator is
        /// registered for a service type which doesn't already have a registered activator group.
        /// </remarks>
        /// <param name="activatorGroup"></param>
        void Register(IActivatorGroup activatorGroup);

        /// <summary>
        /// Registers an activator in the corresponding activator group.
        /// A <see cref="DefaultActivatorGroup"/> will be created if no activator group exists for
        /// its service type.
        /// </summary>
        /// <param name="activator">The activator to register.</param>
        void Register(IActivator activator);

        /// <summary>
        /// Resolves a service for the specified service identity.
        /// </summary>
        /// <param name="identity">The identity of the service to resolve.</param>
        /// <returns>
        /// An activated instance of the requested service identity, if an activator
        /// is registered for the identity; otherwise null.
        /// </returns>
        object Resolve(ServiceIdentity identity);

        /// <summary>
        /// Resolves all services for the specified service type.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns>
        /// Activated instances for all registered services of the specified service type.
        /// An empty enumerable will be returned if no activators have been
        /// registered for the specified service type.
        /// </returns>
        IEnumerable<object> ResolveAll(Type serviceType);

        /// <summary>
        /// Gets the activator for the specified service identity.
        /// </summary>
        /// <param name="identity">The identity of the service to locate an activator for.</param>
        /// <returns>
        /// The registered activator for the specified identity.
        /// If no activator has been registered for the identity a dynamic
        /// activator may be returned for certain requested types.
        /// Null will be returned if no activator has been registered
        /// and if no dynamic activator can be created.
        /// </returns>
        IActivator GetActivator(ServiceIdentity identity);

        /// <summary>
        /// Gets all the activators for the specified service type.
        /// </summary>
        /// <param name="serviceType">The type of service to locate a activators for.</param>
        /// <returns>
        /// All activators for the specified service type.
        /// An empty array will be returned if no activators have been
        /// registered for the specified service type.
        /// </returns>
        IActivator[] GetAllActivators(Type serviceType);
    }
}