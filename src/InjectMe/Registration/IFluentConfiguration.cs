namespace InjectMe.Registration
{
    public interface IFluentConfiguration
    {
        ServiceIdentity Identity { get; }
        IActivatorConfiguration Configuration { get; set; }
    }

    public interface IFluentConfiguration<TService>
    {
        ServiceIdentity Identity { get; }
        IActivatorConfiguration Configuration { get; set; }
    }
}