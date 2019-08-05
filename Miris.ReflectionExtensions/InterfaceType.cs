using System;

namespace Miris.Reflection
{
    /// <summary>
    ///     Decora um determinado <c>Type</c>, identificando-o como um Type de uma interface.
    /// </summary>
    public struct InterfaceType

    {
        #region ' constructor '

        public InterfaceType(Type wrappedType)
        {
            if (wrappedType == null) throw new ArgumentNullException(nameof(wrappedType));

            if (!wrappedType.IsInterface)
                throw new ArgumentException(@"Type isn't a interface.", nameof(wrappedType));

            Type = wrappedType;
        }


        #endregion

        public Type Type { get; }

        #region ' operators '

        public static implicit operator InterfaceType(Type type)
            => new InterfaceType(type);

        public static implicit operator Type(InterfaceType @interface)
            => @interface.Type;

        #endregion
    }
}
