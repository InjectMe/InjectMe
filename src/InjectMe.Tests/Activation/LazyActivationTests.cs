using System;
using InjectMe.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InjectMe.Tests.Activation
{
    [TestClass]
    public class LazyActivationTests : TestBase
    {
        [TestMethod]
        public void ServiceShouldBeActivatedWhenLazyValueIsAccessed()
        {
            // Arrange
            var instanceCreated = false;
            var container = Container.Create(c => c.RegisterSingleton(() =>
            {
                instanceCreated = true;
                return new Foo();
            }));

            // Act
            var lazyFoo = container.ServiceLocator.Resolve<Lazy<Foo>>();
            var createdBeforeValueAccess = instanceCreated;
            var foo = lazyFoo.Value;
            var createdAfterValueAccess = instanceCreated;

            // Assert
            Assert.IsNotNull(foo);
            Assert.IsFalse(createdBeforeValueAccess);
            Assert.IsTrue(createdAfterValueAccess);
        }

        [TestMethod]
        public void LazyShouldNotBeCreatedIfASpecificallyRegistered()
        {
            // Arrange
            var orginalFoo = new Foo();
            var container = Container.Create(c =>
            {
                c.RegisterSingleton<Lazy<Foo>>(() => new Lazy<Foo>(() => orginalFoo));
            });

            // Act
            var lazyFoo = container.ServiceLocator.Resolve<Lazy<Foo>>();
            var foo = lazyFoo.Value;

            // Assert
            Assert.IsNotNull(foo);
            Assert.AreSame(foo, orginalFoo);
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceNotRegisteredException))]
        public void LazyShouldNotBeCreatedIfLazyTypeIsNotRegistered()
        {
            // Arrange
            var container = Container.Create();

            // Act
            container.ServiceLocator.Resolve<Lazy<Foo>>();
        }
    }
}