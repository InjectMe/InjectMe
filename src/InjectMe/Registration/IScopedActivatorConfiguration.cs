using System;
using InjectMe.Activation;
using InjectMe.Construction;

namespace InjectMe.Registration
{
    public interface IScopedActivatorConfiguration : IActivatorConfiguration
    {
        IScopedActivatorConfiguration UsingConcreteType(Type concreteType);
        IScopedActivatorConfiguration UsingConcreteType<TConcrete>();
        IScopedActivatorConfiguration UsingFactory(Func<object> factory);
        IScopedActivatorConfiguration UsingFactory(Func<IActivationContext, object> factory);
        IScopedActivatorConfiguration UsingFactory(IFactory factory);
    }

    public interface IScopedActivatorConfiguration<TService> : IActivatorConfiguration<TService>
    {
        IScopedActivatorConfiguration<TService> UsingConcreteType(Type concreteType);
        IScopedActivatorConfiguration<TService> UsingConcreteType<TConcrete>() where TConcrete : TService;
        IScopedActivatorConfiguration<TService> UsingFactory(Func<TService> factory);
        IScopedActivatorConfiguration<TService> UsingFactory(Func<IActivationContext, TService> factory);
        IScopedActivatorConfiguration<TService> UsingFactory(IFactory factory);
    }
}