using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using InjectMe.Activation;

namespace InjectMe.Diagnostics
{
    class DefaultActivatorGroupDebuggerTypeProxy
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#if NETFX_CORE
        private static readonly FieldInfo ActivatorsField = typeof(DefaultActivatorGroup).GetRuntimeFields().Single(f => f.Name == "_activators");
#else
        private static readonly FieldInfo ActivatorsField = typeof(DefaultActivatorGroup).GetField("_activators", BindingFlags.NonPublic | BindingFlags.Instance);
#endif

        public DefaultActivatorGroupDebuggerTypeProxy(DefaultActivatorGroup activatorGroup)
        {
            Activators = GetActivators(activatorGroup);
            ServiceType = activatorGroup.ServiceType;
        }

        [DebuggerDisplay("Count = {Activators.Length}")]
        public IActivator[] Activators { get; private set; }

        public Type ServiceType { get; private set; }

        private static IActivator[] GetActivators(DefaultActivatorGroup activatorGroup)
        {
            var activators = (IDictionary<ServiceIdentity, IActivator>)ActivatorsField.GetValue(activatorGroup);

            return activators.Values.ToArray();
        }
    }
}