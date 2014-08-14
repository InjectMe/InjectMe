using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using InjectMe.Activation;
using InjectMe.Caching;
using InjectMe.Diagnostics;
using InjectMe.Extensions;
using InjectMe.Registration;

namespace InjectMe
{
    using ActivationContext = Activation.ActivationContext;

    [DebuggerDisplay("Services = {_activatorGroups.Count}")]
    [DebuggerTypeProxy(typeof(ContainerDebuggerTypeProxy))]
    public class Container : IContainer
    {
        private static readonly Type FuncServiceType = typeof(Func<>);
        private static readonly Type FuncContextServiceType = typeof(Func<,>);
        private static readonly Type LazyServiceType = typeof(Lazy<>);
        private static readonly Type EnumerableServiceType = typeof(IEnumerable<>);
        private static readonly Type CollectionServiceType = typeof(ICollection<>);
        private static readonly Type ListServiceType = typeof(IList<>);

        private readonly IDictionary<Type, IActivatorGroup> _activatorGroups = new Dictionary<Type, IActivatorGroup>();

        public IServiceLocator ServiceLocator { get; private set; }

        private Container()
        {
            ServiceLocator = new ServiceLocator(this);

            var singletonCache = new DictionaryServiceCache();

            Configure(config => config.
                RegisterInstance(ServiceLocator).
                RegisterInstance<IContainer>(this).
                RegisterInstance<IServiceCache>(singletonCache));
        }

        public static IContainer Create(Action<IContainerConfiguration> config = null)
        {
            var container = new Container();

            if (config != null)
                container.Configure(config);

            return container;
        }

        public void Configure(Action<IContainerConfiguration> config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            var configuration = new ContainerConfiguration();

            config(configuration);

            configuration.ApplyRegistrations(this);
        }

        public void Register(IActivatorGroup activatorGroup)
        {
            var serviceType = activatorGroup.ServiceType;

            try
            {
                _activatorGroups.Add(serviceType, activatorGroup);
            }
            catch (ArgumentException)
            {
                throw new ActivatorGroupAlreadyRegisteredException(serviceType);
            }
        }

        public void Register(IActivator activator)
        {
            var serviceType = activator.Identity.ServiceType;

            _activatorGroups.
                TryGetValue(serviceType, () => new DefaultActivatorGroup(serviceType)).
                Add(activator);
        }

        public object Resolve(ServiceIdentity identity)
        {
            var activator = GetActivator(identity);
            if (activator == null)
                return null;

            var context = new ActivationContext(this);
            var instance = activator.ActivateService(context);

            return instance;
        }

        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            var activators = GetAllActivators(serviceType);
            if (activators == null)
                return null;

            var context = new ActivationContext(this);

            return from activator in activators
                   select activator.ActivateService(context);
        }

        public IActivator GetActivator(ServiceIdentity identity)
        {
            return
                TryGetRegisteredActivator(identity) ??
                TryCreateGenericActivator(identity) ??
                TryCreateArrayActivator(identity);
        }

        private IActivator TryGetRegisteredActivator(ServiceIdentity identity)
        {
            var activatorGroup = _activatorGroups.TryGetValue(identity.ServiceType);
            if (activatorGroup != null)
                return activatorGroup.GetActivator(identity);

            return null;
        }

        private IActivator TryCreateGenericActivator(ServiceIdentity identity)
        {
            if (identity.ServiceType.IsConstructedGenericType)
            {
                var genericTypeDefinition = identity.ServiceType.GetGenericTypeDefinition();

                var unboundActivatorGroup = _activatorGroups.TryGetValue(genericTypeDefinition);
                if (unboundActivatorGroup != null)
                {
                    var genericArguments = identity.ServiceType.GenericTypeArguments;
                    var boundActivators = unboundActivatorGroup.
                        GetAllActivators().
                        OfType<IUnboundActivator>().
                        Select(a => a.ConstructBoundActivator(genericArguments));

                    foreach (var activator in boundActivators)
                    {
                        Register(activator);
                    }

                    var boundActivator = TryGetRegisteredActivator(identity);
                    if (boundActivator != null)
                        return boundActivator;
                }
                else
                {
                    var realServiceType = identity.ServiceType.GenericTypeArguments[0];

                    if (genericTypeDefinition == FuncServiceType)
                    {
                        var realIdentity = new ServiceIdentity(realServiceType, identity.ServiceName);
                        var realActivator = GetActivator(realIdentity);

                        if (realActivator != null)
                            return new FuncActivator(identity, realActivator);
                    }
                    else if (genericTypeDefinition == LazyServiceType)
                    {
                        var realIdentity = new ServiceIdentity(realServiceType, identity.ServiceName);
                        var realActivator = GetActivator(realIdentity);

                        if (realActivator != null)
                            return new LazyActivator(identity, realActivator);
                    }
                    else if (
                        genericTypeDefinition == EnumerableServiceType ||
                        genericTypeDefinition == CollectionServiceType ||
                        genericTypeDefinition == ListServiceType)
                    {
                        var itemActivators = GetAllActivators(realServiceType);

                        return new ArrayActivator(identity, realServiceType, itemActivators);
                    }
                }
            }

            return null;
        }

        private IActivator TryCreateArrayActivator(ServiceIdentity identity)
        {
            if (identity.ServiceType.IsArray)
            {
                var itemServiceType = identity.ServiceType.GetElementType();
                var itemActivators = GetAllActivators(itemServiceType);

                return new ArrayActivator(identity, itemServiceType, itemActivators);
            }

            return null;
        }

        public IActivator[] GetAllActivators(Type serviceType)
        {
            var activatorGroup = _activatorGroups.TryGetValue(serviceType);

            return (activatorGroup != null)
                ? activatorGroup.GetAllActivators()
                : new IActivator[0];
        }
    }
}