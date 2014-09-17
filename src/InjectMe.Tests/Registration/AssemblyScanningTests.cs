using InjectMe.Registration;
using InjectMe.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InjectMe.Tests.Registration
{
    [TestClass]
    public class AssemblyScanningTests
    {
        [TestMethod]
        public void ServicesShouldBeAutomaticallyRegistered()
        {
            // Arrange
            var container = Container.Create(config =>
            {
                config.Scan(scanner =>
                {
                    scanner.
                        UseDefaultConventions().
                        ScanThisAssembly();
                });
            });

            // Act
            var foo = container.ServiceLocator.Resolve<IFoo>();

            // Assert
            Assert.IsNotNull(foo);
        }

        [TestMethod]
        public void TypesInDefaultFrameworksShouldNotBeLoaded()
        {
            // Act
            var container = Container.Create(config =>
            {
                config.Scan(scanner =>
                {
                    scanner.
                        UseDefaultConventions().
                        ScanLoadedAssemblies(assembly => assembly.ManifestModule.Name != "Moq.dll");
                });
            });
        }
    }
}