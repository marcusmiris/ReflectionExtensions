using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Miris.Reflection
{
    public static class ReflectionExtensions
    {

        /// <remarks>
        ///     É possível usar a classe <see cref="It"/> como helper.
        /// </remarks>
        /// <example>
        ///     GetMethodInfo(() => Enumerable.Any(It.OfType&lt;IEnumerable&gt;)
        /// </example>
        public static MethodInfo GetMethodInfo(Expression<Action> expression)
        {
            var methodCallExpression = expression.Body as MethodCallExpression;
            if (methodCallExpression == null)
                throw new ArgumentException(@"Expressão informada não é um MethodCallExpression válido.");

            return methodCallExpression.Method;
        }

    }
}
