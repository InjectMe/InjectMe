using System;

namespace InjectMe.Caching
{
    public abstract class ServiceScopeAttribute : Attribute
    {
        public IServiceScope Scope { get; set; }

        protected ServiceScopeAttribute()
        {
        }

        protected ServiceScopeAttribute(IServiceScope scope)
        {
            Scope = scope;
        }
    }
}