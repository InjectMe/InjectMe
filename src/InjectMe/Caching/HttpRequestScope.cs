using System.Web;
using InjectMe.Activation;
using InjectMe.Extensions;

namespace InjectMe.Caching
{
    public class HttpRequestScope : IServiceScope
    {
        public IServiceCache GetCache(IInjectionContext context)
        {
            var httpContext = HttpContext.Current;
            if (httpContext == null)
                throw new HttpContextNotSetException();

            var key = GetType();
            var cache = (IServiceCache)httpContext.Items.TryGetValue(key, CreateCache);

            return cache;
        }

        private static object CreateCache()
        {
            return new DictionaryServiceCache();
        }
    }
}