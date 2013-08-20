using InjectMe.Activation;
using InjectMe.Construction;
using InjectMe.Registration;
using InjectMe.Tests.DI.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InjectMe.Tests.DI.Activation
{
    [TestClass]
    public class FactoryActivatorTests : TestBase
    {
        [TestMethod]
        public void SingletonServicesShouldBeCached()
        {
            // Arrange
            var container = Container.Create(c => c.RegisterSingleton<Foo>());

            // Act
            var resolvedObject1 = container.ServiceLocator.Resolve<Foo>();
            var resolvedObject2 = container.ServiceLocator.Resolve<Foo>();

            // Assert
            Assert.IsNotNull(resolvedObject1);
            Assert.IsNotNull(resolvedObject2);
            Assert.AreSame(resolvedObject1, resolvedObject2);
        }

        [TestMethod]
        public void TransientServicesShouldNotBeCached()
        {
            // Arrange
            var container = Container.Create(c => c.RegisterTransient<Foo>());

            // Act
            var resolvedObject1 = container.ServiceLocator.Resolve<Foo>();
            var resolvedObject2 = container.ServiceLocator.Resolve<Foo>();

            // Assert
            Assert.IsNotNull(resolvedObject1);
            Assert.IsNotNull(resolvedObject2);
            Assert.AreNotSame(resolvedObject1, resolvedObject2);
        }

        [TestMethod]
        public void FactoryDelegateShouldBeCalledWhenCreatingService()
        {
            // Arrange
            var originalObject = new Foo();
            var container = Container.Create(c => c.
                Register<Foo>().
                AsTransient().
                UsingFactory(() => originalObject));

            // Act
            var resolvedObject = container.ServiceLocator.Resolve<Foo>();

            // Assert
            Assert.AreSame(originalObject, resolvedObject);
        }

        [TestMethod]
        public void FactoryShouldBeCalledWhenCreatingService()
        {
            // Arrange
            var originalObject = new Foo();
            var factoryMock = CreateMock<IFactory>(m => m.
                Setup(f => f.CreateService(It.IsAny<IActivationContext>())).
                Returns(originalObject));

            var container = Container.Create(c => c.
                Register<Foo>().
                AsTransient().
                UsingFactory(factoryMock.Object));

            // Act
            var resolvedObject = container.ServiceLocator.Resolve<Foo>();

            // Assert
            Assert.AreSame(originalObject, resolvedObject);
        }
    }
}