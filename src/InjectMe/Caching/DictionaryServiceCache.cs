using System;
using System.Collections.Generic;
using InjectMe.Extensions;

namespace InjectMe.Caching
{
    public class DictionaryServiceCache : IServiceCache
    {
        private readonly IDictionary<ServiceIdentity, object> _services = new Dictionary<ServiceIdentity, object>();

        public object Get(ServiceIdentity identity, Func<object> factoryDelegate)
        {
            return _services.TryGetValue(identity, factoryDelegate);
        }

        public void Dispose()
        {
            foreach (var service in _services.Values)
            {
                var disposable = service as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            }

            _services.Clear();
        }
    }
}