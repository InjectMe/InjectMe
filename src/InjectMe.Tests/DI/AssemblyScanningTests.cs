using InjectMe.Registration;
using InjectMe.Tests.DI.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InjectMe.Tests.DI
{
    [TestClass]
    public class AssemblyScanningTests
    {
        [TestMethod]
        public void ServicesShouldBeAutomaticallyRegistered()
        {
            // Arrange
            var container = Container.Create(
                configuration =>
                configuration.Scan(
                    scanner =>
                    scanner.
                        UseBasicConventions().
                        ScanAssemblyContaining<ContainerTests>()));

            // Act
            var foo = container.ServiceLocator.Resolve<IFoo>();

            // Assert
            Assert.IsNotNull(foo);
        }
    }
}