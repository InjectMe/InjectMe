using System.Diagnostics;
using InjectMe.Diagnostics;

namespace InjectMe.Activation
{
    [DebuggerTypeProxy(typeof(InstanceActivatorDebuggerTypeProxy))]
    public class InstanceActivator : IActivator
    {
        public InstanceActivator(ServiceIdentity identity, object instance)
        {
            Identity = identity;
            Instance = instance;
        }

        public ServiceIdentity Identity { get; private set; }
        public object Instance { get; private set; }

        public object ActivateService(IActivationContext context)
        {
            return Instance;
        }
    }
}