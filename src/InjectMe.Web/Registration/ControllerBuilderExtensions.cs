using System.Web.Mvc;
using InjectMe.Web;

// ReSharper disable once CheckNamespace
namespace InjectMe.Registration
{
    public static class ControllerBuilderExtensions
    {
        public static void SetInjectMeControllerFactory(this ControllerBuilder controllerBuilder, IContainer container)
        {
            var controllerFactory = new InjectMeControllerFactory(container);

            controllerBuilder.SetControllerFactory(controllerFactory);
        }
    }
}