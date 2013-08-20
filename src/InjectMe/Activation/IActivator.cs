namespace InjectMe.Activation
{
    public interface IActivator
    {
        ServiceIdentity Identity { get; }
        object ActivateService(IActivationContext context);
    }
}