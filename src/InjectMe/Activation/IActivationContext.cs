using System;
using System.Collections.Generic;

namespace InjectMe.Activation
{
    /// <summary>
    /// Contains information about an active service activation.
    /// </summary>
    public interface IActivationContext : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="InjectMe.IContainer"/> the service is registered in.
        /// </summary>
        IContainer Container { get; }

        /// <summary>
        /// Gets a key/value collection that can be used to share information between different activators during an activation.
        /// </summary>
        IDictionary<object, object> Data { get; }
    }
}