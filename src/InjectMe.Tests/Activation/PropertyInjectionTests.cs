using System;
using InjectMe.Construction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InjectMe.Tests.Activation
{
    [TestClass]
    public class PropertyInjectionTests : TestBase
    {
        [TestMethod]
        public void PropertiesShouldBeInjected()
        {
            // Arrange
            var container = Container.Create(
                configuration =>
                configuration.
                    RegisterInstance(new ConstructionFactorySettings
                    {
                        UsePropertyInjection = true
                    }).
                    RegisterSingleton<Bar>().
                    RegisterSingleton<Foo>());

            // Act
            var bar = container.ServiceLocator.Resolve<Bar>();

            // Assert
            Assert.IsNotNull(bar);
            Assert.IsNotNull(bar.Foo);
        }

        [TestMethod]
        public void PropertiesOnSubTypesShouldBeInjected()
        {
            // Arrange
            var container = Container.Create(
                configuration =>
                configuration.
                    RegisterInstance(new ConstructionFactorySettings
                    {
                        UsePropertyInjection = true
                    }).
                    RegisterSingleton<SuperBar>().
                    RegisterSingleton<Foo>());

            // Act
            var bar = container.ServiceLocator.Resolve<SuperBar>();

            // Assert
            Assert.IsNotNull(bar);
            Assert.IsNotNull(bar.Foo);
        }

        [TestMethod]
        public void PropertiesWithAttributeShouldBeInjected()
        {
            // Arrange
            var container = Container.Create(
                configuration =>
                configuration.
                    RegisterInstance(new ConstructionFactorySettings
                    {
                        PropertyInjectionAttribute = typeof(InjectPropertyAttribute),
                        UsePropertyInjection = true
                    }).
                    RegisterSingleton<BarWithAttribute>().
                    RegisterSingleton<Foo>());

            // Act
            var bar = container.ServiceLocator.Resolve<BarWithAttribute>();

            // Assert
            Assert.IsNotNull(bar);
            Assert.IsNotNull(bar.Foo);
        }

        [TestMethod]
        public void PropertiesShouldOnlyBeInjectedIfSpecified()
        {
            // Arrange
            var container = Container.Create(
                configuration =>
                configuration.
                    RegisterSingleton<Bar>().
                    RegisterSingleton<Foo>());

            // Act
            var bar = container.ServiceLocator.Resolve<Bar>();

            // Assert
            Assert.IsNotNull(bar);
            Assert.IsNull(bar.Foo);
        }

        [TestMethod]
        public void PropertiesShouldOnlyBeInjectedIfCorrectAttributeIsSpecified()
        {
            // Arrange
            var container = Container.Create(
                configuration =>
                configuration.
                    RegisterInstance(new ConstructionFactorySettings
                    {
                        PropertyInjectionAttribute = typeof(InjectPropertyAttribute),
                        UsePropertyInjection = true
                    }).
                    RegisterSingleton<Bar>().
                    RegisterSingleton<Foo>());

            // Act
            var bar = container.ServiceLocator.Resolve<Bar>();

            // Assert
            Assert.IsNotNull(bar);
            Assert.IsNull(bar.Foo);
        }

        private class Foo
        {
        }

        private class Bar
        {
            public Foo Foo { get; private set; }
        }

        private class SuperBar : Bar
        {
        }

        private class BarWithAttribute
        {
            [InjectProperty]
            public Foo Foo { get; private set; }
        }

        private class InjectPropertyAttribute : Attribute
        {
        }
    }
}