using InjectMe.Activation;
using InjectMe.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InjectMe.Tests.Activation
{
    [TestClass]
    public class DefaultActivatorGroupTests : TestBase
    {
        [TestMethod]
        public void ActivatorShouldBeReturnedEvenIfNameDoesntMatchWhenRequestingDefaultService()
        {
            // Arrange
            var serviceType = typeof(IFoo);
            var activatorGroup = new DefaultActivatorGroup(serviceType);
            var defaultServiceIdentity = new ServiceIdentity(serviceType);
            var specialServiceIdentity = new ServiceIdentity(serviceType, "Special");
            var specialActivator = new InstanceActivator(specialServiceIdentity, null);

            activatorGroup.Add(specialActivator);

            // Act
            var defaultActivator = activatorGroup.GetActivator(defaultServiceIdentity);

            // Assert
            Assert.AreSame(specialActivator, defaultActivator);
        }
    }
}