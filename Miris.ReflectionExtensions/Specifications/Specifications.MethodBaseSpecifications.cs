using System;
using System.Reflection;

namespace Miris.Reflection
{
    public static partial class ReflectionSpecifications
    {
        /// <summary>
        ///     Valida a quantidade de parãmetros do método.
        /// </summary>
        /// <param name="n">
        ///     Quantidade de parâmetros esperada.
        /// </param>
        public static ISpecification<MethodBase> WithNParameters(int n) =>
            new DirectSpecification<MethodBase>(m => m.GetParameters().Length == n);

        public static ISpecification<MethodBase> ParameterOfType(uint parameterIndex, Type type) =>
            new DirectSpecification<MethodBase>(m =>
            {
                var parameters = m.GetParameters();
                if (parameters.Length < parameterIndex + 1) return false;

                var paramType = parameters[parameterIndex].ParameterType;
                return paramType == type
                    || (type.IsGenericTypeDefinition && paramType.IsGenericType && !paramType.IsGenericTypeDefinition && paramType.GetGenericTypeDefinition() == type)
                    ;
            });

        public static ISpecification<MethodBase> FirstParameterOfType(Type type)
            => ParameterOfType(0, type);

        public static ISpecification<MethodBase> SecondParameterOfType(Type type)
            => ParameterOfType(1, type);
    }
}
