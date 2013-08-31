using System;
using System.Collections.Generic;
using InjectMe.Caching;

namespace InjectMe.Registration
{
    public class ContainerConfiguration : IContainerConfiguration
    {
        private static readonly IServiceScope TransientScope = new TransientScope();
        private static readonly IServiceScope SingletonScope = new SingletonScope();

        private readonly Queue<IActivatorConfiguration> _activatorConfigurations = new Queue<IActivatorConfiguration>();

        public IContainerConfiguration Register(IActivatorConfiguration activatorConfiguration)
        {
            if (activatorConfiguration == null)
                throw new ArgumentNullException("activatorConfiguration");

            _activatorConfigurations.Enqueue(activatorConfiguration);

            return this;
        }

        public IContainerConfiguration Register(Type serviceType, Action<IFluentConfiguration> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            var fluentConfiguration = Register(serviceType);

            action(fluentConfiguration);

            return this;
        }

        public IContainerConfiguration Register<TService>(Action<IFluentConfiguration<TService>> action)
            where TService : class
        {
            if (action == null)
                throw new ArgumentNullException("action");

            var fluentConfiguration = Register<TService>();

            action(fluentConfiguration);

            return this;
        }

        public IFluentConfiguration Register(ServiceIdentity identity)
        {
            return new FluentConfiguration(identity, this);
        }

        public IFluentConfiguration Register(Type serviceType, string serviceName = null)
        {
            var identity = new ServiceIdentity(serviceType, serviceName);

            return Register(identity);
        }

        public IFluentConfiguration<TService> Register<TService>(string serviceName = null)
            where TService : class
        {
            return new FluentConfiguration<TService>(serviceName, this);
        }

        public IContainerConfiguration RegisterInstance(Type serviceType, object instance, string serviceName = null)
        {
            var identity = new ServiceIdentity(serviceType, serviceName);
            var activatorConfiguration = new InstanceActivatorConfiguration<object>(identity, instance);

            return Register(activatorConfiguration);
        }

        public IContainerConfiguration RegisterInstance<TService>(TService instance, string serviceName = null)
            where TService : class
        {
            var serviceType = typeof(TService);
            var identity = new ServiceIdentity(serviceType, serviceName);
            var activatorConfiguration = new InstanceActivatorConfiguration<TService>(identity, instance);

            return Register(activatorConfiguration);
        }

        public IContainerConfiguration RegisterScoped<TService>(IServiceScope scope, string serviceName = null)
            where TService : class
        {
            Register<TService>(serviceName).
                InScope(scope);

            return this;
        }

        public IContainerConfiguration RegisterScoped<TService>(IServiceScope scope, Func<TService> factory, string serviceName = null)
            where TService : class
        {
            Register<TService>(serviceName).
                InScope(scope).
                UsingFactory(factory);

            return this;
        }

        public IContainerConfiguration RegisterScoped<TService, TConcrete>(IServiceScope scope, string serviceName = null)
            where TConcrete : TService
            where TService : class
        {
            Register<TService>(serviceName).
                InScope(scope).
                UsingConcreteType<TConcrete>();

            return this;
        }

        public IContainerConfiguration RegisterSingleton<TService>(string serviceName = null)
            where TService : class
        {
            return RegisterScoped<TService>(SingletonScope, serviceName);
        }

        public IContainerConfiguration RegisterSingleton<TService>(Func<TService> factory, string serviceName = null)
            where TService : class
        {
            return RegisterScoped(SingletonScope, factory, serviceName);
        }

        public IContainerConfiguration RegisterSingleton<TService, TConcrete>(string serviceName = null)
            where TConcrete : TService
            where TService : class
        {
            return RegisterScoped<TService, TConcrete>(SingletonScope, serviceName);
        }

        public IContainerConfiguration RegisterTransient<TService>(string serviceName = null)
            where TService : class
        {
            return RegisterScoped<TService>(TransientScope, serviceName);
        }

        public IContainerConfiguration RegisterTransient<TService>(Func<TService> factory, string serviceName = null)
            where TService : class
        {
            return RegisterScoped(TransientScope, factory, serviceName);
        }

        public IContainerConfiguration RegisterTransient<TService, TConcrete>(string serviceName = null)
            where TConcrete : TService
            where TService : class
        {
            return RegisterScoped<TService, TConcrete>(TransientScope, serviceName);
        }

        public IContainerConfiguration Scan(Action<IAssemblyScanner> scanner)
        {
            if (scanner == null)
                throw new ArgumentNullException("scanner");

            var scan = new AssemblyScanner(this);

            scanner(scan);

            return this;
        }

        public void ApplyRegistrations(IContainer container)
        {
            while (_activatorConfigurations.Count > 0)
            {
                var activator = _activatorConfigurations.
                    Dequeue().
                    GetActivator(container);

                container.Register(activator);
            }
        }
    }
}