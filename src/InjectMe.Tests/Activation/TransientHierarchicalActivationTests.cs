using InjectMe.Construction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InjectMe.Tests.Activation
{
    [TestClass]
    public class TransientHierarchicalActivationTests : TestBase
    {
        [TestMethod]
        public void HierarchicalTransientServicesShouldActivateTheSameInstance()
        {
            // Arrange
            var container = Container.Create(c =>
            {
                c.RegisterInstance(new ConstructionFactorySettings { UsePropertyInjection = true });
                c.RegisterTransient<Foo>();
                c.RegisterTransient<Bar>();
                c.RegisterTransient<Baz>();
            });

            // Act
            var foo = container.ServiceLocator.Resolve<Foo>();
            var bar = container.ServiceLocator.Resolve<Bar>();
            var baz = container.ServiceLocator.Resolve<Baz>();

            // Assert
            Assert.AreSame(foo.Baz, foo.Bar.Baz);
            Assert.AreNotSame(bar.Baz, baz);
        }

        class Foo
        {
            public Bar Bar { get; set; }
            public Baz Baz { get; set; }
        }

        class Bar
        {
            public Baz Baz { get; set; }
        }

        class Baz
        {
        }
    }
}