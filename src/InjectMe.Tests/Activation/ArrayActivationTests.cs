using InjectMe.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InjectMe.Tests.Activation
{
    [TestClass]
    public class ArrayActivationTests : TestBase
    {
        [TestMethod]
        public void ActivatedArrayShouldContainInstancesOfAllServices()
        {
            // Arrange
            var container = Container.Create(c =>
            {
                c.RegisterSingleton<IFoo, DefaultFoo>();
                c.RegisterSingleton<IFoo, SpecialFoo>("Special");
            });

            // Act
            var fooArray = container.ServiceLocator.Resolve<IFoo[]>();

            // Assert
            Assert.IsNotNull(fooArray);
            Assert.AreEqual(2, fooArray.Length);
            Assert.IsInstanceOfType(fooArray[0], typeof(DefaultFoo));
            Assert.IsInstanceOfType(fooArray[1], typeof(SpecialFoo));
        }

        [TestMethod]
        public void RegisteredArrayShouldNotActivateRegisteredServices()
        {
            // Arrange
            var container = Container.Create(c =>
            {
                c.RegisterSingleton<IFoo[]>(() => new IFoo[0]);
                c.RegisterSingleton<IFoo, DefaultFoo>();
                c.RegisterSingleton<IFoo, SpecialFoo>("Special");
            });

            // Act
            var fooArray = container.ServiceLocator.Resolve<IFoo[]>();

            // Assert
            Assert.IsNotNull(fooArray);
            Assert.AreEqual(0, fooArray.Length);
        }

        [TestMethod]
        public void EmptyArrayShouldBeCreatedIfNoArrayTypeIsRegistered()
        {
            // Arrange
            var container = Container.Create();

            // Act
            var fooArray = container.ServiceLocator.Resolve<IFoo[]>();

            // Assert
            Assert.IsNotNull(fooArray);
            Assert.AreEqual(0, fooArray.Length);
        }

        class DefaultFoo : IFoo { }
        class SpecialFoo : IFoo { }
    }
}