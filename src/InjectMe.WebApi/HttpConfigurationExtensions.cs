using System.Web.Http;

namespace InjectMe.WebApi
{
    public static class HttpConfigurationExtensions
    {
        public static void SetInjectMeDependencyResolver(this HttpConfiguration httpConfiguration, IContainer container)
        {
            httpConfiguration.DependencyResolver = new InjectMeDependencyResolver(container);
        }
    }
}