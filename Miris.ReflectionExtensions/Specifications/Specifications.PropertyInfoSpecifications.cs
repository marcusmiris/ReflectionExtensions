using System.Reflection;

namespace Miris.Reflection
{
    public static partial class ReflectionSpecifications
    {
        /// <summary>
        ///     Satisfeita para propriedade não-estáticas.
        /// </summary>
        public static ISpecification<PropertyInfo> OfType<T>() =>
            new DirectSpecification<PropertyInfo>(p => p.PropertyType == typeof(T));

    }
}