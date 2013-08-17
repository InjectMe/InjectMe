using System;
using System.Collections.Generic;

namespace InjectMe.Activation
{
    public interface IInjectionContext : IDisposable
    {
        IContainer Container { get; }
        IDictionary<object, object> Data { get; }
    }
}