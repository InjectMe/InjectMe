using System.Web.Mvc;
using InjectMe.Web;

// ReSharper disable once CheckNamespace
namespace InjectMe.Registration
{
    public static class ControllerBuilderExtensions
    {
        public static void SetInjectMeControllerFactory(this ControllerBuilder controllerBuilder, IContainer container)
        {
            SetInjectMeControllerFactory(controllerBuilder, container.ServiceLocator);
        }

        public static void SetInjectMeControllerFactory(this ControllerBuilder controllerBuilder, IServiceLocator serviceLocator)
        {
            var controllerFactory = new InjectMeControllerFactory(serviceLocator);

            controllerBuilder.SetControllerFactory(controllerFactory);
        }
    }
}