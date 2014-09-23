using System;
using System.Web;
using InjectMe.Activation;
using InjectMe.Extensions;

namespace InjectMe.Caching
{
    public class HttpRequestScope : IServiceScope
    {
        private static readonly Type RequestItemKey = typeof(HttpRequestScope);

        public IServiceCache GetCache(IActivationContext context)
        {
            var httpContext = HttpContext.Current;
            if (httpContext == null)
                throw new HttpContextNotSetException();

            var cache = (IServiceCache)httpContext.Items.TryGetValue(RequestItemKey, CreateCache);

            return cache;
        }

        private static object CreateCache()
        {
            return new DictionaryServiceCache();
        }
    }
}