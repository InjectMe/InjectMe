using System;
using InjectMe.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InjectMe.Tests.Activation
{
    [TestClass]
    public class FuncActivationTests : TestBase
    {
        [TestMethod]
        public void ServiceShouldBeActivatedWhenDelegateIsInvoked()
        {
            // Arrange
            var instanceCreated = false;
            var container = Container.Create(c => c.RegisterSingleton(() =>
            {
                instanceCreated = true;
                return new Foo();
            }));

            // Act
            var fooDelegate = container.ServiceLocator.Resolve<Func<Foo>>();
            var createdBeforeInvocation = instanceCreated;
            var foo = fooDelegate();
            var createdAfterInvocation = instanceCreated;

            // Assert
            Assert.IsNotNull(foo);
            Assert.IsFalse(createdBeforeInvocation);
            Assert.IsTrue(createdAfterInvocation);
        }

        [TestMethod]
        public void DelegateShouldNotBeCreatedIfAFuncIsRegistered()
        {
            // Arrange
            var orginalFoo = new Foo();
            var container = Container.Create(c =>
            {
                c.RegisterSingleton<Func<Foo>>(() => () => orginalFoo);
            });

            // Act
            var fooDelegate = container.ServiceLocator.Resolve<Func<Foo>>();
            var foo = fooDelegate();

            // Assert
            Assert.IsNotNull(foo);
            Assert.AreSame(foo, orginalFoo);
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceNotRegisteredException))]
        public void DelegateShouldNotBeCreatedIfDelegateTypeIsNotRegistered()
        {
            // Arrange
            var container = Container.Create();

            // Act
            container.ServiceLocator.Resolve<Func<Foo>>();
        }
    }
}