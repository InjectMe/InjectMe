using System;
using System.Collections.Generic;

namespace InjectMe.Activation
{
    public class ActivationContext : IActivationContext
    {
        public IContainer Container { get; private set; }
        public IDictionary<object, object> Data { get; private set; }

        public ActivationContext(IContainer container)
        {
            Container = container;
            Data = new Dictionary<object, object>();
        }

        public void Dispose()
        {
            foreach (var value in Data.Values)
            {
                var disposable = value as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            }
        }
    }
}