using System.Web.Http;
using InjectMe.WebApi;

// ReSharper disable once CheckNamespace
namespace InjectMe.Registration
{
    public static class HttpConfigurationExtensions
    {
        public static void SetInjectMeDependencyResolver(this HttpConfiguration httpConfiguration, IContainer container)
        {
            httpConfiguration.DependencyResolver = new InjectMeDependencyResolver(container);
        }
    }
}