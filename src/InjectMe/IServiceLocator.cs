using System;

namespace InjectMe
{
    /// <summary>
    /// Provides methods to explicitly resolve services in an <see cref="InjectMe.IContainer"/>.
    /// </summary>
    public interface IServiceLocator
    {
        /// <summary>
        /// Resolves a service of type <paramref name="serviceType"/> with the specified <paramref name="serviceName"/>.
        /// </summary>
        /// <param name="serviceType">
        /// The type of service to resolve.
        /// </param>
        /// <param name="serviceName">
        /// The name of the service to resolve.
        /// </param>
        /// <returns>
        /// An activated service.
        /// </returns>
        /// <exception cref="ServiceNotRegisteredException">
        /// No service of the requested identity has been registered in the container.
        /// </exception>
        /// <remarks>
        /// Returns the default service if no <paramref name="serviceName"/> is specified.
        /// </remarks>
        object Resolve(Type serviceType, string serviceName = null);

        /// <summary>
        /// Resolves a service of type <typeparamref name="T"/> with the specified <paramref name="serviceName"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of service to resolve.
        /// </typeparam>
        /// <param name="serviceName">
        /// The name of the service to resolve.
        /// </param>
        /// <returns>
        /// An activated service.
        /// </returns>
        /// <exception cref="ServiceNotRegisteredException">
        /// No service of the requested identity has been registered in the container.
        /// </exception>
        /// <remarks>
        /// Returns the default service if no <paramref name="serviceName"/> is specified.
        /// </remarks>
        T Resolve<T>(string serviceName = null) where T : class;

        /// <summary>
        /// Resolves all services of type <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">
        /// The type of service to resolve.
        /// </param>
        /// <returns>
        /// An array of activated services.
        /// </returns>
        /// <exception cref="ServiceNotRegisteredException">
        /// No service of the requested identity has been registered in the container.
        /// </exception>
        object[] ResolveAll(Type serviceType);

        /// <summary>
        /// Resolves all services of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of service to resolve.
        /// </typeparam>
        /// <returns>
        /// An array of activated services.
        /// </returns>
        /// <exception cref="ServiceNotRegisteredException">
        /// No service of the requested identity has been registered in the container.
        /// </exception>
        T[] ResolveAll<T>() where T : class;

        /// <summary>
        /// Resolves a service of type <paramref name="serviceType"/> with the specified <paramref name="serviceName"/>.
        /// </summary>
        /// <param name="serviceType">
        /// The type of service to resolve.
        /// </param>
        /// <param name="serviceName">
        /// The name of the service to resolve.
        /// </param>
        /// <returns>
        /// An ativated service, if one has been registered for the specified identity; otherwise null.
        /// </returns>
        /// <remarks>
        /// Returns the default service if no <paramref name="serviceName"/> is specified.
        /// </remarks>
        object TryResolve(Type serviceType, string serviceName = null);

        /// <summary>
        /// Resolves a service of type <typeparamref name="T"/> with the specified <paramref name="serviceName"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of service to resolve.
        /// </typeparam>
        /// <param name="serviceName">
        /// The name of the service to resolve.
        /// </param>
        /// <returns>
        /// An ativated service, if one has been registered for the specified identity; otherwise null.
        /// </returns>
        /// <remarks>
        /// Returns the default service if no <paramref name="serviceName"/> is specified.
        /// </remarks>
        T TryResolve<T>(string serviceName = null) where T : class;

        /// <summary>
        /// Resolves all services of type <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">
        /// The type of service to resolve.
        /// </param>
        /// <returns>
        /// An array of activated services, if any has been registered for type <paramref name="serviceType"/>; otherwise an empty array.
        /// </returns>
        object[] TryResolveAll(Type serviceType);

        /// <summary>
        /// Resolves all services of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of service to resolve.
        /// </typeparam>
        /// <returns>
        /// An array of activated services, if any has been registered for type <typeparamref name="T"/>; otherwise an empty array.
        /// </returns>
        T[] TryResolveAll<T>() where T : class;
    }
}