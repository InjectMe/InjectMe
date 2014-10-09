using System;
using System.Linq;

namespace InjectMe
{
    /// <summary>
    /// Provides methods to explicitly resolve services in an <see cref="InjectMe.IContainer"/>.
    /// </summary>
    public class ServiceLocator : IServiceLocator
    {
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of the ServiceLocator class using the specified <paramref name="container"/>.
        /// </summary>
        /// <param name="container">
        /// The container to resolve services in.
        /// </param>
        public ServiceLocator(IContainer container)
        {
            _container = container;
        }

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
        public object Resolve(Type serviceType, string serviceName)
        {
            var instance = TryResolve(serviceType, serviceName);
            if (instance == null)
                throw new ServiceNotRegisteredException(serviceType, serviceName);

            return instance;
        }

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
        public T Resolve<T>(string serviceName) where T : class
        {
            var serviceType = typeof(T);
            var instance = (T)Resolve(serviceType, serviceName);

            return instance;
        }

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
        public object[] ResolveAll(Type serviceType)
        {
            var instances = _container.ResolveAll(serviceType);
            if (instances == null)
                throw new ServiceNotRegisteredException(serviceType);
            
            return instances.ToArray();
        }

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
        public T[] ResolveAll<T>() where T : class
        {
            var serviceType = typeof(T);
            var instances = _container.ResolveAll(serviceType);
            if (instances == null)
                throw new ServiceNotRegisteredException(serviceType);

            return instances.
                Cast<T>().
                ToArray();
        }

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
        public object TryResolve(Type serviceType, string serviceName)
        {
            var identity = new ServiceIdentity(serviceType, serviceName);
            var instance = _container.Resolve(identity);

            return instance;
        }

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
        public T TryResolve<T>(string serviceName) where T : class
        {
            var serviceType = typeof(T);
            var instance = (T)TryResolve(serviceType, serviceName);

            return instance;
        }

        /// <summary>
        /// Resolves all services of type <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">
        /// The type of service to resolve.
        /// </param>
        /// <returns>
        /// An array of activated services, if any has been registered for type <paramref name="serviceType"/>; otherwise an empty array.
        /// </returns>
        public object[] TryResolveAll(Type serviceType)
        {
            var instances = _container.ResolveAll(serviceType);
            if (instances == null)
                return new object[0];

            return instances.ToArray();
        }

        /// <summary>
        /// Resolves all services of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of service to resolve.
        /// </typeparam>
        /// <returns>
        /// An array of activated services, if any has been registered for type <typeparamref name="T"/>; otherwise an empty array.
        /// </returns>
        public T[] TryResolveAll<T>() where T : class
        {
            var serviceType = typeof(T);
            var instances = _container.ResolveAll(serviceType);
            if (instances == null)
                return new T[0];

            return instances.
                Cast<T>().
                ToArray();
        }
    }
}