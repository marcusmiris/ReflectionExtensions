using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Miris.Reflection.Tests
{

    [TestClass]
    public class TypeExtensionsTests
    {
        /// <summary>
        ///     Garante que o método <see cref="TypeExtensions.IsFromGenericTypeDefinition"/>
        ///     está funcionando devidamente.
        /// </summary>
        [TestMethod]
        public void IsFromGenericTypeDefinitionUnitTest()
        {
            // from classes
            Assert.IsFalse(typeof(object).IsFromGenericTypeDefinition(typeof(List<>)));
            Assert.IsFalse(typeof(ArrayList).IsFromGenericTypeDefinition(typeof(List<>)));
            Assert.IsTrue(typeof(List<int>).IsFromGenericTypeDefinition(typeof(List<>)));
            Assert.IsTrue(typeof(List<string>).IsFromGenericTypeDefinition(typeof(List<>)));
            Assert.IsTrue(typeof(List<string>).IsFromGenericTypeDefinition(typeof(List<>)));

            // from interfaces
            Assert.IsFalse(typeof(object).IsFromGenericTypeDefinition(typeof(IList<>)));
            Assert.IsFalse(typeof(ArrayList).IsFromGenericTypeDefinition(typeof(IList<>)));
            Assert.IsTrue(typeof(List<int>).IsFromGenericTypeDefinition(typeof(IList<>)));
            Assert.IsTrue(typeof(List<string>).IsFromGenericTypeDefinition(typeof(IList<>)));
            Assert.IsTrue(typeof(List<string>).IsFromGenericTypeDefinition(typeof(IList<>)));
        }

        [TestMethod]
        public void ImplementsUnitTest()
        {
            // objects
            Assert.IsFalse(TypeExtensions.Implements(typeof(object), typeof(IList<>)));
            Assert.IsFalse(TypeExtensions.Implements(typeof(ArrayList), typeof(IList<>)));
            Assert.IsTrue(TypeExtensions.Implements(typeof(List<int>), typeof(IList<>)));
            Assert.IsTrue(TypeExtensions.Implements(typeof(List<string>), typeof(IList<>)));
            Assert.IsTrue(TypeExtensions.Implements(typeof(List<string>), typeof(IList<string>)));
            Assert.IsTrue(TypeExtensions.Implements(typeof(List<string>), typeof(IList)));

            // interfaces
            Assert.IsFalse(TypeExtensions.Implements(typeof(IEnumerable), typeof(IList)));
            Assert.IsFalse(TypeExtensions.Implements(typeof(IEnumerable), typeof(ICustomFormatter)));
            Assert.IsTrue(TypeExtensions.Implements(typeof(IC), typeof(IA)));
            Assert.IsTrue(TypeExtensions.Implements(typeof(ID), typeof(IA<>)));
            Assert.IsTrue(TypeExtensions.Implements(typeof(ID), typeof(IA<int>)));
            Assert.IsTrue(TypeExtensions.Implements(typeof(IList<>), typeof(IList<>)));
            Assert.IsTrue(TypeExtensions.Implements(typeof(IList<int>), typeof(IList<>)));
        }

        #region
        public interface IA { }
        public interface IB : IA { }
        public interface IC : IB { }

        public interface IA<T> { }

        public interface IB<T> : IA<int> { }
        public interface ID : IB<string> { }
        #endregion
    }
}
