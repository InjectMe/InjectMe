using InjectMe.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InjectMe.Tests
{
    [TestClass]
    public class ContainerTests
    {
        [TestMethod]
        public void ServiceLocatorShouldAutomaticallyBeRegistered()
        {
            // Arrange
            var container = Container.Create();

            // Act
            var resolvedServiceLocator = container.ServiceLocator.Resolve<IServiceLocator>();

            // Assert
            Assert.IsNotNull(resolvedServiceLocator);
        }

        [TestMethod]
        public void ContainerShouldAutomaticallyBeRegistered()
        {
            // Arrange
            var container = Container.Create();

            // Act
            var resolvedContainer = container.ServiceLocator.Resolve<IContainer>();

            // Assert
            Assert.IsNotNull(resolvedContainer);
        }

        [TestMethod]
        public void SingletonCacheShouldAutomaticallyBeRegistered()
        {
            // Arrange
            var container = Container.Create();

            // Act
            var resolvedCache = container.ServiceLocator.Resolve<IServiceCache>();

            // Assert
            Assert.IsNotNull(resolvedCache);
        }
    }
}