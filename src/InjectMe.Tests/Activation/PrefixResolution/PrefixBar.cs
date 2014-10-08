using InjectMe.Tests.TestData;

namespace InjectMe.Tests.Activation.PrefixResolution
{
    class PrefixBar : IBar
    {
        public PrefixBar(IFoo prefixFoo)
        {
            Foo = prefixFoo;
        }

        public IFoo Foo { get; private set; }
    }
}