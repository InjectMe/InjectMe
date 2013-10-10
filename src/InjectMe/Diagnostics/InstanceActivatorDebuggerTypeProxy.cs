using InjectMe.Activation;

namespace InjectMe.Diagnostics
{
    class InstanceActivatorDebuggerTypeProxy
    {
        private readonly InstanceActivator _activator;

        public InstanceActivatorDebuggerTypeProxy(InstanceActivator activator)
        {
            _activator = activator;
        }

        public ServiceIdentity Identity { get { return _activator.Identity; } }
        public object Instance { get { return _activator.Instance; } }
    }
}