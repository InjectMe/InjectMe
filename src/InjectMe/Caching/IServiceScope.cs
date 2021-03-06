using InjectMe.Activation;

namespace InjectMe.Caching
{
    public interface IServiceScope
    {
        IServiceCache GetCache(IActivationContext context);
    }
}