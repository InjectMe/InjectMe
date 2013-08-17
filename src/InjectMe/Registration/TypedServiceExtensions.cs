using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using InjectMe.Activation;

namespace InjectMe.Registration
{
    public static class TypedServiceExtensions
    {
        public static Func<IInjectionContext, object> GetTypedFuncDelegate(this IActivator activator)
        {
            return CallGenericMethod("GetTypedFuncDelegate", activator.Identity.ServiceType, activator);
        }

        public static Func<IInjectionContext, object> GetTypedLazyDelegate(this IActivator activator)
        {
            return CallGenericMethod("GetTypedLazyDelegate", activator.Identity.ServiceType, activator);
        }

        public static Func<IInjectionContext, object> GetTypedArrayDelegate(this IEnumerable<IActivator> activators, Type serviceType)
        {
            return CallGenericMethod("GetTypedArrayDelegate", serviceType, activators);
        }

        private static Func<IInjectionContext, object> CallGenericMethod(string methodName, Type genericType, params object[] arguments)
        {
            var method = typeof(TypedServiceExtensions).
                GetTypeInfo().
                DeclaredMethods.Single(m => m.Name == methodName && m.IsGenericMethod).
                MakeGenericMethod(genericType);

            return (Func<IInjectionContext, object>)method.Invoke(null, arguments);
        }

        // ReSharper disable UnusedMember.Local
        private static Func<IInjectionContext, object> GetTypedFuncDelegate<T>(IActivator activator)
        {
            return context => new Func<T>(() => (T)activator.ActivateService(context));
        }

        private static Func<IInjectionContext, object> GetTypedLazyDelegate<T>(IActivator activator)
        {
            return context => new Lazy<T>(() => (T)activator.ActivateService((context)));
        }

        private static Func<IInjectionContext, object> GetTypedArrayDelegate<T>(this IEnumerable<IActivator> activators)
        {
            return context => activators.
                Select(s => s.ActivateService(context)).
                Cast<T>().
                ToArray();
        }
        // ReSharper restore UnusedMember.Local
    }
}