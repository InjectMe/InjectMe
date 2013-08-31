using InjectMe.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InjectMe.Tests.Activation
{
    [TestClass]
    public class InstanceActivatorTests
    {
        [TestMethod]
        public void InstanceServiceShouldReturnSpecifiedInstance()
        {
            // Arrange
            var originalObject = new Foo();
            var container = Container.Create(c => c.RegisterInstance(originalObject));

            // Act
            var resolvedObject = container.ServiceLocator.Resolve<Foo>();

            // Assert
            Assert.AreSame(originalObject, resolvedObject);
        }
    }
}