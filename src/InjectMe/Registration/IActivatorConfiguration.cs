using InjectMe.Activation;

namespace InjectMe.Registration
{
    public interface IActivatorConfiguration
    {
        IActivator GetActivator(IContainer container);
    }

    public interface IActivatorConfiguration<TService>
    {
        IActivator GetActivator(IContainer container);
    }
}