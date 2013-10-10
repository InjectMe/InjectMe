using InjectMe.Registration;
using InjectMe.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InjectMe.Tests.Activation
{
    [TestClass]
    public class UnboundActivatorTests : TestBase
    {
        [TestMethod]
        public void UnboundTypesShouldResolveForAllTypes()
        {
            // Arrange
            var container = Container.Create(
                configuration =>
                {
                    configuration.RegisterSingleton<IFoo, Foo>();
                    configuration.
                        Register(typeof(IGen<>)).
                        AsTransient().
                        UsingConcreteType(typeof(Gen<>));
                });

            // Act
            var resolvedObject = container.ServiceLocator.Resolve<IGen<IFoo>>();

            // Assert
            Assert.IsNotNull(resolvedObject);
            Assert.IsNotNull(resolvedObject.Item);
        }

        [TestMethod]
        public void AutoRegisteredUnboundTypesShouldResolveForAllTypes()
        {
            // Arrange
            var container = CreateContainerForCurrentAssembly();

            // Act
            var resolvedObject1 = container.ServiceLocator.Resolve<IGen<IFoo>>();
            var resolvedObject2 = container.ServiceLocator.Resolve<IGen<IFoo, int>>();
            var resolvedObject3 = container.ServiceLocator.Resolve<IGen<double, int>>();
            var resolvedObject4 = container.ServiceLocator.Resolve<IGen<IFoo, IBar>>();
            var resolvedObject5 = container.ServiceLocator.Resolve<IGen<IFoo, IGen<IBar>>>();

            // Assert
            Assert.IsNotNull(resolvedObject1);
            Assert.IsNotNull(resolvedObject1.Item);

            Assert.IsNotNull(resolvedObject2);
            Assert.IsNotNull(resolvedObject2.Item1);

            Assert.IsNotNull(resolvedObject3);

            Assert.IsNotNull(resolvedObject4);
            Assert.IsNotNull(resolvedObject4.Item1);
            Assert.IsNotNull(resolvedObject4.Item2);

            Assert.IsNotNull(resolvedObject5);
            Assert.IsNotNull(resolvedObject5.Item1);
            Assert.IsNotNull(resolvedObject5.Item2);
            Assert.IsNotNull(resolvedObject5.Item2.Item);
        }

        private static IContainer CreateContainerForCurrentAssembly()
        {
            return Container.Create(
                configuration => configuration.Scan(
                    scanner =>
                    {
                        scanner.UseDefaultConventions();
                        scanner.ScanThisAssembly();
                    }));
        }
    }
}