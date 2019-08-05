using System;
using System.Reflection;

namespace Miris.Reflection
{
    public static partial class ReflectionSpecifications
    {
        /// <summary>
        ///     Satisfeita para membros estáticos.
        /// </summary>
        public static ISpecification<MemberInfo> Static =>
            new DirectSpecification<MemberInfo>(member =>
            {
                var p = member as PropertyInfo;
                if (p != null) return ((p.GetGetMethod() ?? p.GetSetMethod())?.IsStatic).GetValueOrDefault();

                var m = member as MethodBase;
                if (m != null) return m.IsStatic;

                throw new NotImplementedException($"Método não implementado para membros do tipo { member.GetType() }.");
            });

        /// <summary>
        ///     Satisfeita para propriedade não-estáticas.
        /// </summary>
        public static ISpecification<MemberInfo> NonStatic => new NotSpecification<MemberInfo>(Static);

        public static ISpecification<MemberInfo> Named(string name) =>
            new DirectSpecification<MemberInfo>(p => p.Name.Equals(name));

    }
}
