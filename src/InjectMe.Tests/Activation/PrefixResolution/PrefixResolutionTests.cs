using InjectMe.Construction;
using InjectMe.Registration;
using InjectMe.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InjectMe.Tests.Activation.PrefixResolution
{
    [TestClass]
    public class PrefixResolutionTests
    {
        [TestMethod]
        public void PrefixedTypeShouldBeResolvedUsingPrefixResolution()
        {
            // Arrange
            var container = CreateContainer(true);

            // Act
            var bar = container.ServiceLocator.Resolve<IBar>("Prefix");

            // Assert
            var typeName = bar.Foo.GetType().Name;

            Assert.AreEqual("PrefixFoo", typeName);
        }

        [TestMethod]
        public void PrefixedTypeShouldNotBeResolvedWithoutPrefixResolution()
        {
            // Arrange
            var container = CreateContainer(false);

            // Act
            var bar = container.ServiceLocator.Resolve<IBar>("Prefix");

            // Assert
            var typeName = bar.Foo.GetType().Name;

            Assert.AreEqual("Foo", typeName);
        }

        private static IContainer CreateContainer(bool usePrefixResolution)
        {
            return Container.Create(configuration =>
            {
                configuration.
                    RegisterInstance(new ConstructionFactorySettings
                    {
                        UsePrefixResolution = usePrefixResolution
                    }).
                    Scan(scanner =>
                    {
                        scanner.
                            UseDefaultConventions().
                            ScanThisAssembly();
                    });
            });
        }
    }
}