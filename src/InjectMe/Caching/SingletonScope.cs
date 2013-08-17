using InjectMe.Activation;

namespace InjectMe.Caching
{
    public class SingletonScope : IServiceScope
    {
        public IServiceCache GetCache(IInjectionContext context)
        {
            return context.Container.ServiceLocator.Resolve<IServiceCache>();
        }
    }
}