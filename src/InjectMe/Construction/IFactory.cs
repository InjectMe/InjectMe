using InjectMe.Activation;

namespace InjectMe.Construction
{
    public interface IFactory
    {
        object CreateService(IActivationContext context);
    }
}