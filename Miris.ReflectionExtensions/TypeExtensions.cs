using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Miris.Reflection.ReflectionSpecifications;

namespace Miris.Reflection
{
    public static class TypeExtensions
    {
        /// <summary>
        ///     Identifica se <see cref="candidate"/> implementa <see cref="@interface"/>.
        /// </summary>
        public static bool Implements(
            this Type candidate,
            InterfaceType @interface)
        {
            if (candidate == typeof(object)) return false; // break
            if (candidate == @interface) return true;

            return @interface.Type.IsGenericTypeDefinition
                ? candidate.IsFromGenericTypeDefinition(@interface.Type)
                : candidate.GetInterfaces().Any(i => i.Implements(@interface));
        }

        public static MethodInfo GetMethod(
            this Type type,
            params ISpecification<MethodInfo>[] specifications) => GetMethods(type, specifications).Single();

        public static IEnumerable<MethodInfo> GetMethods(
            this Type type,
            params ISpecification<MethodInfo>[] specifications)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type.GetMethods()
                .Where(prop => prop.SatisfiesAll(specifications));
        }

        public static IEnumerable<PropertyInfo> GetProperties(
            this Type type,
            params ISpecification<PropertyInfo>[] specifications)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type.GetProperties()
                .Where(prop => prop.SatisfiesAll(specifications));
        }

        public static bool AnyProperty(
            this Type type,
            params ISpecification<PropertyInfo>[] specifications)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type.GetProperties()
                .Any(prop => prop.SatisfiesAll(specifications));
        }


        /// <summary>
        ///     Determina se o tipo candidato herda do generic type definition informado.
        ///     Por exemplo, determina que uma instância de List&lt;int&gt; é do tipo List&lt;&gt;
        /// </summary>
        public static bool IsFromGenericTypeDefinition(this Type candidate, Type genericDefinition)
        {
            if (!genericDefinition.IsGenericTypeDefinition)
                throw new ArgumentException($"O tipo '{genericDefinition.Name}' não é um Generic Type Definition válido.");

            if (candidate == null) throw new ArgumentNullException(nameof(candidate));

            while (candidate != null)    // tail recursion
            {

                if (candidate == typeof(object)) return false; // break

                if (candidate.IsGenericType && candidate.GetGenericTypeDefinition() == genericDefinition)
                    return true;

                if (genericDefinition.IsInterface && candidate.GetInterfaces().Select(intfType => IsFromGenericTypeDefinition(intfType, genericDefinition)).Any(eh => eh))
                    return true;

                candidate = candidate.BaseType;
            }

            return false;
        }
    }
}
