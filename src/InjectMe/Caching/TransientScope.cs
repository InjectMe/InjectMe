using InjectMe.Activation;
using InjectMe.Extensions;

namespace InjectMe.Caching
{
    public class TransientScope : IServiceScope
    {
        public IServiceCache GetCache(IInjectionContext context)
        {
            var key = GetType();
            var cache = (IServiceCache)context.Data.TryGetValue(key, CreateCache);

            return cache;
        }

        private static object CreateCache()
        {
            return new DictionaryServiceCache();
        }
    }
}