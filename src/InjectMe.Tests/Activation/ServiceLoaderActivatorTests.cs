using InjectMe.Activation;
using InjectMe.Registration;
using InjectMe.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InjectMe.Tests.Activation
{
    [TestClass]
    public class ServiceLoaderActivatorTests
    {
        [TestMethod]
        public void ServiceLoadersShouldRespectScopeOfDependencies()
        {
            // Arrange
            var container = Container.Create(c =>
            {
                c.RegisterTransient<IFoo, Foo>();
                c.RegisterTransient<IServiceLoader<IBar>, BarServiceLoader>();
                c.Register(new ServiceLoaderActivatorConfiguration(typeof(IBar)));
            });

            // Act
            var bar1 = container.ServiceLocator.Resolve<IBar>();
            var bar2 = container.ServiceLocator.Resolve<IBar>();

            // Assert
            Assert.AreNotSame(bar1, bar2);
            Assert.AreNotSame(bar1.Foo, bar2.Foo);
        }

        class BarServiceLoader : IServiceLoader<IBar>
        {
            private readonly IFoo _foo;

            public BarServiceLoader(IFoo foo)
            {
                _foo = foo;
            }

            public IBar LoadService()
            {
                return new Bar(_foo);
            }
        }
    }
}