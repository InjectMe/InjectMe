using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using InjectMe.Activation;

namespace InjectMe.Registration
{
    public static class TypedServiceExtensions
    {
        private static readonly MethodInfo TypedFuncMethod;
        private static readonly MethodInfo TypedLazyDelegate;
        private static readonly MethodInfo TypedArrayDelegate;

        static TypedServiceExtensions()
        {
            var methods = typeof(TypedServiceExtensions).
                GetTypeInfo().
                DeclaredMethods.
                ToArray();

            TypedFuncMethod = methods.Single(m => m.Name == "GetTypedFuncDelegate" && m.IsGenericMethod);
            TypedLazyDelegate = methods.Single(m => m.Name == "GetTypedLazyDelegate" && m.IsGenericMethod);
            TypedArrayDelegate = methods.Single(m => m.Name == "GetTypedArrayDelegate" && m.IsGenericMethod);
        }

        public static Func<IActivationContext, object> GetTypedFuncDelegate(this IActivator activator)
        {
            return CallGenericMethod(TypedFuncMethod, activator.Identity.ServiceType, activator);
        }

        public static Func<IActivationContext, object> GetTypedLazyDelegate(this IActivator activator)
        {
            return CallGenericMethod(TypedLazyDelegate, activator.Identity.ServiceType, activator);
        }

        public static Func<IActivationContext, object> GetTypedArrayDelegate(this IEnumerable<IActivator> activators, Type serviceType)
        {
            return CallGenericMethod(TypedArrayDelegate, serviceType, activators);
        }

        private static Func<IActivationContext, object> CallGenericMethod(MethodInfo method, Type genericType, params object[] arguments)
        {
            return (Func<IActivationContext, object>)method.
                MakeGenericMethod(genericType).
                Invoke(null, arguments);
        }

        // ReSharper disable UnusedMember.Local
        private static Func<IActivationContext, object> GetTypedFuncDelegate<T>(IActivator activator)
        {
            return context => new Func<T>(() => (T)activator.ActivateService(context));
        }

        private static Func<IActivationContext, object> GetTypedLazyDelegate<T>(IActivator activator)
        {
            return context => new Lazy<T>(() => (T)activator.ActivateService((context)));
        }

        private static Func<IActivationContext, object> GetTypedArrayDelegate<T>(this IEnumerable<IActivator> activators)
        {
            return context => activators.
                Select(s => s.ActivateService(context)).
                Cast<T>().
                ToArray();
        }
        // ReSharper restore UnusedMember.Local
    }
}