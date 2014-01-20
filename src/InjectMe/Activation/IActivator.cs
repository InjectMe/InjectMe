namespace InjectMe.Activation
{
    /// <summary>
    /// Defines the contract 
    /// The activator is resposible for the activation of a service.
    /// </summary>
    public interface IActivator
    {
        /// <summary>
        /// Gets the <see cref="ServiceIdentity"/> of the service to activate an instance for.
        /// </summary>
        ServiceIdentity Identity { get; }

        /// <summary>Activates the service identified by <see cref="Identity"/>.</summary>
        /// <param name="context">The <see cref="IActivationContext"/> for the executing activation.</param>
        /// <returns>An instance of the specified service.</returns>
        object ActivateService(IActivationContext context);
    }
}