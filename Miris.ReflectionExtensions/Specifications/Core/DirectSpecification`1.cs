using System;
using System.ComponentModel;

namespace Miris.Reflection
{
    public static partial class ReflectionSpecifications
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public sealed class DirectSpecification<T> : Specification<T>
        {
            #region Members

            readonly Func<T, bool> _predicate;

            #endregion

            #region Constructor

            public DirectSpecification(Func<T, bool> predicate)
            {
                if (predicate == null) throw new ArgumentNullException(nameof(predicate));

                _predicate = predicate;
            }

            #endregion

            #region Override

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override bool SatisfiedBy(T candidate)
            {
                return _predicate.Invoke(candidate);
            }

            #endregion
        }
    }
}