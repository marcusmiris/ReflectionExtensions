using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using static Miris.Reflection.ReflectionSpecifications;
using static System.Reflection.BindingFlags;

namespace Miris.Reflection
{
    [DebuggerStepThrough]
    public static class ObjectExtensions
    {

        #region ' GetPrivateMemberValue '

        public static TField GetPrivateMemberValue<TField>(
            this object @source,
            string fieldName)
        {
            return (TField)@source.GetPrivateMemberValue(fieldName);
        }

        /// <summary>
        ///     Returns a private Property Value from a given Object. Uses Reflection.
        ///     Throws a ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <param name="obj">Object from where the Property Value is returned</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <returns>PropertyValue</returns>
        public static object GetPrivateMemberValue(this object obj, string propName)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrWhiteSpace(propName)) throw new ArgumentNullException(nameof(propName));
            //
            var navigationProperties = propName.Split('.');
            var candidate = obj;
            //
            foreach (var navProp in navigationProperties)
            {
                var t = candidate.GetType();
                FieldInfo fieldInfo = null;
                PropertyInfo propertyInfo = null;

                while (fieldInfo == null && propertyInfo == null && t != null)
                {
                    fieldInfo = t.GetField(navProp, Public | NonPublic | Instance);
                    if (fieldInfo == null)
                    {
                        propertyInfo = t.GetProperty(navProp, Public | NonPublic | Instance);
                    }

                    t = t.BaseType;
                }
                if (fieldInfo == null && propertyInfo == null)
                {
                    throw new MissingMemberException(candidate.GetType().FullName, navProp);
                }

                candidate = fieldInfo != null
                    ? fieldInfo.GetValue(candidate)
                    : propertyInfo.GetValue(candidate, null);
            }

            return candidate;
        }

        #endregion

        #region ' SetPrivateMemberValue '

        public static void SetPrivateMemberValue(this object obj, string propName, object newValue)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrWhiteSpace(propName)) throw new ArgumentNullException(nameof(propName));


            var x = Regex.Match(propName, @"((?<NavProperty>.*)\.)?(?<MemberName>.*)");

            // navega até o objeto cujo atributo será modificado.
            var navProperty = x.Groups["NavProperty"];
            if (navProperty.Success) obj = GetPrivateMemberValue(obj, navProperty.Value);

            // altera o atributo privado;
            var navProp = x.Groups["MemberName"].Value;

            FieldInfo fieldInfo = null;
            PropertyInfo propertyInfo = null;
            var t = obj.GetType();

            while (fieldInfo == null && propertyInfo == null && t != null)
            {
                fieldInfo = t.GetField(navProp, Public | NonPublic | Instance);
                if (fieldInfo == null)
                {
                    propertyInfo = t.GetProperty(navProp, Public | NonPublic | Instance);
                }

                t = t.BaseType;
            }
            if (fieldInfo == null && propertyInfo == null)
            {
                throw new MissingMemberException(obj.GetType().FullName, navProp);
            }
                    
            if (fieldInfo != null)
                fieldInfo.SetValue(obj, newValue);
            else
                propertyInfo.SetValue(obj, newValue, null);
        }

        public static void SetPrivateMemberValue<TNewValue>(
            this object obj, string propName, TNewValue newValue)
            => SetPrivateMemberValue(obj, propName, (object)newValue);

        #endregion

        #region ' InvokePrivateAction '

        public static void InvokePrivateAction(
            this object candidate,
            string methodName,
            object[] arguments)
        {
            if (candidate == null) throw new NullReferenceException();
            //
            var methodInfo = candidate.GetType().GetMethod(methodName, Instance | NonPublic);
            if (methodInfo == null)
            {
                throw new MissingMethodException($"Method '{ candidate.GetType().FullName }.{  methodName }({ string.Join(", ", arguments?.Select(_ => _.GetType().FullName)) })' not found.");
            }

            methodInfo.Invoke(candidate, arguments);
        }

        #endregion

        public static IEnumerable<PropertyInfo> GetProperties<TObject>(
            this TObject @object,
            params ISpecification<PropertyInfo>[] specifications)
        {
            if (@object == null) throw new ArgumentNullException(nameof(@object));

            var type = (@object as Type) ?? @object.GetType();

            return TypeExtensions.GetProperties(type, specifications);
        }

    }
}
