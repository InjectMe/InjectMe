using System;
using System.Collections.Generic;

namespace InjectMe.Registration
{
    public class ContainerConfiguration : IContainerConfiguration
    {
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

            var fluentRegistration = Register(serviceType);

            action(fluentRegistration);

            return this;
        }

        public IContainerConfiguration Register<TService>(Action<IFluentConfiguration<TService>> action)
            where TService : class
        {
            if (action == null)
                throw new ArgumentNullException("action");

            var fluentRegistration = Register<TService>();

            action(fluentRegistration);

            return this;
        }

        public IFluentConfiguration Register(ServiceIdentity identity)
        {
            return new FluentConfiguration(identity, _activatorConfigurations);
        }

        public IFluentConfiguration Register(Type serviceType, string serviceName = null)
        {
            var identity = new ServiceIdentity(serviceType, serviceName);

            return new FluentConfiguration(identity, _activatorConfigurations);
        }

        public IFluentConfiguration<TService> Register<TService>(string serviceName = null)
            where TService : class
        {
            return new FluentConfiguration<TService>(serviceName, _activatorConfigurations);
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