using InjectMe.Caching;

namespace InjectMe.Registration
{
    public static partial class FactoryActivatorRegistrationExtensions
    {
        private static readonly IServiceScope HttpRequestScope = new HttpRequestScope();
        private static readonly IServiceScope HttpSessionScope = new HttpSessionScope();

        public static IFactoryActivatorRegistration InHttpRequestScope(this IFluentConfiguration fluentRegistration)
        {
            return InScope(fluentRegistration, HttpRequestScope);
        }

        public static IFactoryActivatorRegistration<TService> InHttpRequestScope<TService>(this IFluentConfiguration<TService> fluentConfiguration)
            where TService : class
        {
            return InScope(fluentConfiguration, HttpRequestScope);
        }

        public static IFactoryActivatorRegistration InHttpSessionScope(this IFluentConfiguration fluentRegistration)
        {
            return InScope(fluentRegistration, HttpSessionScope);
        }

        public static IFactoryActivatorRegistration<TService> InHttpSessionScope<TService>(this IFluentConfiguration<TService> fluentConfiguration)
            where TService : class
        {
            return InScope(fluentConfiguration, HttpSessionScope);
        }
    }
}