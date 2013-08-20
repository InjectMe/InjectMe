using InjectMe.Caching;

namespace InjectMe.Registration
{
    public static partial class FactoryActivatorRegistrationExtensions
    {
        private static readonly IServiceScope TransientScope = new TransientScope();
        private static readonly IServiceScope SingletonScope = new SingletonScope();

        public static IContainerConfiguration RegisterScoped<TService>(this IContainerConfiguration configuration, IServiceScope scope, string serviceName = null)
            where TService : class
        {
            configuration.
                Register<TService>(serviceName).
                InScope(scope);

            return configuration;
        }

        public static IContainerConfiguration RegisterScoped<TService, TConcrete>(this IContainerConfiguration configuration, IServiceScope scope, string serviceName = null)
            where TConcrete : TService
            where TService : class
        {
            configuration.
                Register<TService>(serviceName).
                InScope(scope).
                UsingConcreteType(typeof(TConcrete));

            return configuration;
        }

        public static IContainerConfiguration RegisterSingleton<TService>(this IContainerConfiguration configuration, string serviceName = null)
            where TService : class
        {
            return RegisterScoped<TService>(configuration, SingletonScope, serviceName);
        }

        public static IContainerConfiguration RegisterSingleton<TService, TConcrete>(this IContainerConfiguration configuration, string serviceName = null)
            where TConcrete : TService
            where TService : class
        {
            return RegisterScoped<TService, TConcrete>(configuration, SingletonScope, serviceName);
        }

        public static IContainerConfiguration RegisterTransient<TService>(this IContainerConfiguration configuration, string serviceName = null)
            where TService : class
        {
            return RegisterScoped<TService>(configuration, TransientScope, serviceName);
        }

        public static IContainerConfiguration RegisterTransient<TService, TConcrete>(this IContainerConfiguration configuration, string serviceName = null)
            where TConcrete : TService
            where TService : class
        {
            return RegisterScoped<TService, TConcrete>(configuration, TransientScope, serviceName);
        }

        public static IFactoryActivatorRegistration AsSingleton(this IFluentConfiguration fluentRegistration)
        {
            return InScope(fluentRegistration, SingletonScope);
        }

        public static IFactoryActivatorRegistration<TService> AsSingleton<TService>(this IFluentConfiguration<TService> fluentConfiguration)
            where TService : class
        {
            return InScope(fluentConfiguration, SingletonScope);
        }

        public static IFactoryActivatorRegistration AsTransient(this IFluentConfiguration fluentRegistration)
        {
            return InScope(fluentRegistration, TransientScope);
        }

        public static IFactoryActivatorRegistration<TService> AsTransient<TService>(this IFluentConfiguration<TService> fluentConfiguration)
            where TService : class
        {
            return InScope(fluentConfiguration, TransientScope);
        }

        public static IFactoryActivatorRegistration InScope(this IFluentConfiguration fluentRegistration, IServiceScope serviceScope)
        {
            var registration = new FactoryActivatorRegistration<object>(fluentRegistration.Identity, serviceScope);

            fluentRegistration.Configuration = registration;

            return registration;
        }

        public static IFactoryActivatorRegistration<TService> InScope<TService>(this IFluentConfiguration<TService> fluentConfiguration, IServiceScope serviceScope)
        {
            var registration = new FactoryActivatorRegistration<TService>(fluentConfiguration.Identity, serviceScope);

            fluentConfiguration.Configuration = registration;

            return registration;
        }
    }
}