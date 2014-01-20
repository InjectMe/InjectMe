using System;
using System.Collections.Generic;
using InjectMe.Activation;
using InjectMe.Registration;

namespace InjectMe
{
    /// <summary>
    /// The container is responsible for keeping track of all registered services.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Gets a reference to a service locator for the container.
        /// </summary>
        IServiceLocator ServiceLocator { get; }

        /// <summary>
        /// Add registrations to the container.
        /// </summary>
        /// <param name="configure"></param>
        void Configure(Action<IContainerConfiguration> configure = null);

        /// <summary>
        /// Registers an activator group in the container.
        /// The activator group is registered groups service type.
        /// </summary>
        /// <remarks>
        /// A <see cref="DefaultActivatorGroup"/> is automatically created when an activator is
        /// registered for a service type which doesn't already have a registered activator group.
        /// </remarks>
        /// <param name="activatorGroup"></param>
        void Register(IActivatorGroup activatorGroup);

        /// <summary>
        /// Registers an activator in the corresponding activator group.
        /// A <see cref="DefaultActivatorGroup"/> will be created if no activator group exists for
        /// the service type.
        /// </summary>
        /// <param name="activator">The activator to register.</param>
        void Register(IActivator activator);
        object Resolve(ServiceIdentity identity);
        IEnumerable<object> ResolveAll(Type serviceType);

        /// <summary>
        /// Gets the activator for the specified service identity.
        /// </summary>
        /// <param name="identity">The identity of the service to locate an activator for.</param>
        /// <returns></returns>
        IActivator GetActivator(ServiceIdentity identity);

        /// <summary>
        /// Gets all the activators for the specified service type.
        /// </summary>
        /// <param name="serviceType">The type of service to locate a activators for.</param>
        /// <returns>Activators for the specified service type.</returns>
        IActivator[] GetAllActivators(Type serviceType);
    }
}