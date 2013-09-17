using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace InjectMe.Web
{
    public class InjectMeControllerFactory : DefaultControllerFactory
    {
        private readonly IContainer _container;

        public InjectMeControllerFactory(IContainer container)
        {
            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType != null)
            {
                var controller = _container.ServiceLocator.TryResolve(controllerType) as IController;
                if (controller != null)
                    return controller;
            }

            return base.GetControllerInstance(requestContext, null);
        }
    }
}
