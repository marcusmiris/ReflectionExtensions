using System;
using System.Reflection;

namespace Miris.ReflectionExtensions
{
    public static class FieldInfoExtensions
    {

        #region ' FieldInfo.GetValue<T> '

        public static T GetValue<T>(
            this FieldInfo fieldInfo,
            object obj = null)
        {
            if (fieldInfo == null) throw new NullReferenceException();

            // verifica se os tipos são compatíveis
            if (!typeof(T).IsAssignableFrom(fieldInfo.FieldType))
            {
                throw new InvalidCastException($"O valor retornado pelo field `{ fieldInfo.Name }` (`{ fieldInfo.FieldType.FullName }`) é incompatível com o tipo esperado (`{ typeof(T).FullName }`).");
            }

            var result = fieldInfo.GetValue(obj);
            return (T)result;
        }

        #endregion

    }
}
