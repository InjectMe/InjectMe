using System.Web.Mvc;

namespace InjectMe.Web
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