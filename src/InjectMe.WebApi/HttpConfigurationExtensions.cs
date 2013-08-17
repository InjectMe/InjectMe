using System.Web.Http;
using System.Web.Http.Dependencies;

namespace InjectMe.WebApi
{
    public static class HttpConfigurationExtensions
    {
        public static void SetDomoDependencyResolver(this HttpConfiguration httpConfiguration, IContainer container)
        {
            var dependencyResolver = container.ServiceLocator.Resolve<IDependencyResolver>("InjectMe");

            httpConfiguration.DependencyResolver = dependencyResolver;
        }
    }
}