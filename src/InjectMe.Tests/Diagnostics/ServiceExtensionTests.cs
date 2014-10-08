using InjectMe.Diagnostics;
using InjectMe.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InjectMe.Tests.Diagnostics
{
    [TestClass]
    public class ServiceExtensionTests
    {
        [TestMethod]
        public void DisplayNameShouldContainAssemblyAndTypeName()
        {
            // Arrange
            var fooClassType = typeof(Foo);
            var fooInterfaceType = typeof(IFoo);

            // Act
            var fooClassText = fooClassType.GetDisplayName();
            var fooInterfaceText = fooInterfaceType.GetDisplayName();

            // Assert
            Assert.AreEqual("InjectMe.Tests.TestData.Foo", fooClassText);
            Assert.AreEqual("InjectMe.Tests.TestData.IFoo", fooInterfaceText);
        }

        [TestMethod]
        public void DisplayNameForGenericsShouldContainTypeArguments()
        {
            // Act
            var genIntType = typeof(Gen<int>);
            var genIntFooType = typeof(Gen<int, Foo>);

            // Arrange
            var genIntText = genIntType.GetDisplayName();
            var genIntFooText = genIntFooType.GetDisplayName();

            // Assert
            Assert.AreEqual("InjectMe.Tests.TestData.Gen<System.Int32>", genIntText);
            Assert.AreEqual("InjectMe.Tests.TestData.Gen<System.Int32, InjectMe.Tests.TestData.Foo>", genIntFooText);
        }

        [TestMethod]
        public void DisplayNameForTypeDefinitionsShouldContainNameOfTypeArguments()
        {
            // Act
            var gen1 = typeof(Gen<>);
            var gen2 = typeof(Gen<,>);

            // Arrange
            var gen1Text = gen1.GetDisplayName();
            var gen2Text = gen2.GetDisplayName();

            // Assert
            Assert.AreEqual("InjectMe.Tests.TestData.Gen<T>", gen1Text);
            Assert.AreEqual("InjectMe.Tests.TestData.Gen<T1, T2>", gen2Text);
        }
    }
}