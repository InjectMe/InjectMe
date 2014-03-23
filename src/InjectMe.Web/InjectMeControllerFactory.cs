using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace InjectMe.Web
{
    public class InjectMeControllerFactory : DefaultControllerFactory
    {
        private readonly IServiceLocator _serviceLocator;

        public InjectMeControllerFactory(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType != null)
            {
                var controller = _serviceLocator.TryResolve(controllerType) as IController;
                if (controller != null)
                    return controller;
            }

            return base.GetControllerInstance(requestContext, null);
        }
    }
}
