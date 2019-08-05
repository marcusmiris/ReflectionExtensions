using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Linq.Expressions;
using static Miris.Reflection.ReflectionSpecifications;

namespace Miris.Reflection.Tests
{
    [TestClass]
    public class ExpressionExtensionsTests
    {
        [TestMethod]
        public void GetMethodInfoUnitTest()
        {
            var result = ReflectionExtensions.GetMethodInfo(() =>
                It.OfType<IQueryable<object>>().Any(It.OfType<Expression<Func<object, bool>>>())
            );

            var exprected = typeof(Queryable)
                .GetMethod(Named("Any"), WithNParameters(2))
                .MakeGenericMethod(typeof(object));

            Assert.AreEqual(exprected, result);
        }
    }
}
