using InjectMe.Activation;

namespace InjectMe.Registration
{
    public interface IActivatorConfiguration
    {
        IActivator GetActivator(IContainer container);
    }
}