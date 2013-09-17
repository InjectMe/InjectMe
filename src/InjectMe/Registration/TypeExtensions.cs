using System.Reflection;
using InjectMe.Caching;

namespace InjectMe.Registration
{
    public static class TypeExtensions
    {
        public static IServiceScope GetServiceScope(this TypeInfo type)
        {
            var scopeAttribute = type.GetCustomAttribute<ServiceScopeAttribute>();
            if (scopeAttribute != null)
                return scopeAttribute.Scope;

            return null;
        }
    }
}