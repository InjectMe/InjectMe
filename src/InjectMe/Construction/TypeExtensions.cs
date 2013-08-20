using System;

namespace InjectMe.Construction
{
    using ActivationContext = Activation.ActivationContext;

    public static class TypeExtensions
    {
        public static object ConstructInstance(this Type type, IContainer container)
        {
            var factory = ConstructionFactory.Create(type, container);
            var context = new ActivationContext(container);
            var instance = factory.CreateService(context);

            return instance;
        }
    }
}