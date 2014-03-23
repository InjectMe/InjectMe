using System.Web.Http;
using InjectMe.WebApi;

// ReSharper disable once CheckNamespace
namespace InjectMe.Registration
{
    public static class HttpConfigurationExtensions
    {
        public static void SetInjectMeDependencyResolver(this HttpConfiguration httpConfiguration, IContainer container)
        {
            SetInjectMeDependencyResolver(httpConfiguration, container.ServiceLocator);
        }

        public static void SetInjectMeDependencyResolver(this HttpConfiguration httpConfiguration, IServiceLocator serviceLocator)
        {
            httpConfiguration.DependencyResolver = new InjectMeDependencyResolver(serviceLocator);
        }
    }
}