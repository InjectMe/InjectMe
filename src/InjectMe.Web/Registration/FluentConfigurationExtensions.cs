using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InjectMe.Caching;

namespace InjectMe.Registration
{
    public static class FluentConfigurationExtensions
    {
        private static readonly IServiceScope HttpRequestScope = new HttpRequestScope();
        private static readonly IServiceScope HttpSessionScope = new HttpSessionScope();

        public static IScopedActivatorConfiguration InHttpRequestScope(this IFluentConfiguration configuration)
        {
            return configuration.InScope(HttpRequestScope);
        }

        public static IScopedActivatorConfiguration InHttpSessionScope(this IFluentConfiguration configuration)
        {
            return configuration.InScope(HttpSessionScope);
        }

        public static IScopedActivatorConfiguration<TService> InHttpRequestScope<TService>(this IFluentConfiguration<TService> configuration)
            where TService : class
        {
            return configuration.InScope(HttpRequestScope);
        }

        public static IScopedActivatorConfiguration<TService> InHttpSessionScope<TService>(this IFluentConfiguration<TService> configuration)
            where TService : class
        {
            return configuration.InScope(HttpSessionScope);
        }
    }
}
