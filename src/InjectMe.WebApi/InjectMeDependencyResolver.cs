using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace InjectMe.WebApi
{
    public class InjectMeDependencyResolver : IDependencyResolver
    {
        private readonly IServiceLocator _serviceLocator;

        public InjectMeDependencyResolver(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            return _serviceLocator.TryResolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _serviceLocator.ResolveAll(serviceType);
        }

        public void Dispose()
        {
        }
    }
}