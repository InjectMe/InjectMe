namespace InjectMe.Tests.TestData
{
    public class Bar : IBar
    {
        public IFoo Foo { get; private set; }

        public Bar(IFoo foo)
        {
            Foo = foo;
        }
    }
}