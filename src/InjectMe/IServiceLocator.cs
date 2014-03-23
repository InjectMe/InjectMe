using System;

namespace InjectMe
{
    public interface IServiceLocator
    {
        T[] ResolveAll<T>() where T : class;
        T[] TryResolveAll<T>() where T : class;
        T Resolve<T>(string serviceName = null) where T : class;
        T TryResolve<T>(string serviceName = null) where T : class;

        object[] ResolveAll(Type serviceType);
        object[] TryResolveAll(Type serviceType);
        object Resolve(Type serviceType, string serviceName = null);
        object TryResolve(Type serviceType, string serviceName = null);
    }
}