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

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            return _container.ServiceLocator.Resolve<IController>(controllerName);
        }
    }
}
