using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Miris.Reflection
{
    public static class MethodInfoExtensions
    {
        public static T CreateDelegate<T>(this MethodInfo method) where T : class
        {
            return Delegate.CreateDelegate(typeof(T), method) as T;
        }
        public static Action<T1, T2> BuildAction<T1, T2>(this MethodInfo method)
        {
            var obj = Expression.Parameter(typeof(object), "o");
            var value = Expression.Parameter(typeof(object));

            Expression<Action<T1, T2>> expr =
                Expression.Lambda<Action<T1, T2>>(
                    Expression.Call(
                        Expression.Convert(obj, method.DeclaringType),
                        method,
                        Expression.Convert(value, method.GetParameters()[0].ParameterType)),
                    obj,
                    value);

            return expr.Compile();
        }
    }
}