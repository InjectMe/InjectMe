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
    }
}