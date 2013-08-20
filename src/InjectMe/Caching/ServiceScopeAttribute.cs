using System;

namespace InjectMe.Caching
{
    public abstract class ServiceScopeAttribute : Attribute
    {
        public IServiceScope Scope { get; private set; }

        protected ServiceScopeAttribute(IServiceScope serviceScope)
        {
            Scope = serviceScope;
        }
    }
}