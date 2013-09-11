using System.Web.Http;
using System.Web.Http.Dependencies;

namespace InjectMe.WebApi
{
    public static class HttpConfigurationExtensions
    {
        private const string ServiceName = "InjectMe";

        public static void SetInjectMeDependencyResolver(this HttpConfiguration httpConfiguration, IContainer container)
        {
            httpConfiguration.DependencyResolver = GetDependencyResolver(container);
        }

        private static IDependencyResolver GetDependencyResolver(IContainer container)
        {
            var resolver = container.ServiceLocator.TryResolve<IDependencyResolver>(ServiceName);
            if (resolver == null)
            {
                container.Configure(c => c.RegisterSingleton<IDependencyResolver, InjectMeDependencyResolver>(ServiceName));

                resolver = container.ServiceLocator.Resolve<IDependencyResolver>(ServiceName);
            }

            return resolver;
        }
    }
}