using System;
using System.Collections.Generic;

namespace InjectMe.Activation
{
    public interface IActivationContext : IDisposable
    {
        IContainer Container { get; }
        IDictionary<object, object> Data { get; }
    }
}