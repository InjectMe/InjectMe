using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using InjectMe.Activation;

namespace InjectMe.Diagnostics
{
    public class ContainerDebuggerTypeProxy
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#if NETFX_CORE
        private static readonly FieldInfo ActivatorGroupsField = typeof(Container).GetRuntimeField("_activatorGroups");
#else
        private static readonly FieldInfo ActivatorGroupsField = typeof(Container).GetField("_activatorGroups", BindingFlags.NonPublic | BindingFlags.Instance);
#endif

        public ContainerDebuggerTypeProxy(Container container)
        {
            ActivatorGroups = GetActivatorGroups(container);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public IActivatorGroup[] ActivatorGroups { get; private set; }

        private static IActivatorGroup[] GetActivatorGroups(Container container)
        {
            var activatorGroups = (IDictionary<Type, IActivatorGroup>)ActivatorGroupsField.GetValue(container);

            return activatorGroups.Values.ToArray();
        }
    }
}