using InjectMe.Caching;

namespace InjectMe.Registration
{
    public interface IFluentConfiguration
    {
        ServiceIdentity Identity { get; }
        IActivatorConfiguration Configuration { get; set; }

        IScopedActivatorConfiguration AsSingleton();
        IScopedActivatorConfiguration AsTransient();
        IScopedActivatorConfiguration InScope(IServiceScope serviceScope);
        IInstanceActivatorConfiguration UsingInstance(object instance);
    }

    public interface IFluentConfiguration<TService>
    {
        ServiceIdentity Identity { get; }
        IActivatorConfiguration Configuration { get; set; }

        IScopedActivatorConfiguration<TService> AsSingleton();
        IScopedActivatorConfiguration<TService> AsTransient();
        IScopedActivatorConfiguration<TService> InScope(IServiceScope serviceScope);
        IInstanceActivatorConfiguration<TService> UsingInstance(TService instance);
    }
}