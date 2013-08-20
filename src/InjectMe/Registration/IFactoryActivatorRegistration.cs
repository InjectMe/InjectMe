using System;
using InjectMe.Activation;
using InjectMe.Construction;

namespace InjectMe.Registration
{
    public interface IFactoryActivatorRegistration : IActivatorRegistration
    {
        IFactoryActivatorRegistration UsingConcreteType(Type concreteType);
        IFactoryActivatorRegistration UsingConcreteType<TConcrete>();
        IFactoryActivatorRegistration UsingFactory(Func<object> factoryDelegate);
        IFactoryActivatorRegistration UsingFactory(Func<IActivationContext, object> factoryDelegate);
        IFactoryActivatorRegistration UsingFactory(IFactory factory);
    }

    public interface IFactoryActivatorRegistration<TService> : IActivatorRegistration<TService>
    {
        IFactoryActivatorRegistration<TService> UsingConcreteType(Type concreteType);
        IFactoryActivatorRegistration<TService> UsingConcreteType<TConcrete>() where TConcrete : TService;
        IFactoryActivatorRegistration<TService> UsingFactory(Func<TService> factoryDelegate);
        IFactoryActivatorRegistration<TService> UsingFactory(Func<IActivationContext, TService> factoryDelegate);
        IFactoryActivatorRegistration<TService> UsingFactory(IFactory factory);
    }
}