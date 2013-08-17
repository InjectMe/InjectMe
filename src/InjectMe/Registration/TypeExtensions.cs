using System.Reflection;
using InjectMe.Caching;

namespace InjectMe.Registration
{
    public static class TypeExtensions
    {
        public static IServiceScope GetServiceScope(this TypeInfo type)
        {
            var serviceTypeAttribute = type.GetCustomAttribute<ServiceScopeAttribute>();
            if (serviceTypeAttribute != null)
                return serviceTypeAttribute.Scope;

            return null;
        }
    }
}