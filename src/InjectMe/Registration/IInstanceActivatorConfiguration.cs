namespace InjectMe.Registration
{
    public interface IInstanceActivatorConfiguration : IActivatorConfiguration
    {
        object Instance { get; }
    }

    public interface IInstanceActivatorConfiguration<TService> : IActivatorConfiguration<TService>
    {
        TService Instance { get; }
    }
}