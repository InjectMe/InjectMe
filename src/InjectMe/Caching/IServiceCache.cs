using System;

namespace InjectMe.Caching
{
    public interface IServiceCache : IDisposable
    {
        object Get(ServiceIdentity identity, Func<object> factoryDelegate);
    }
}