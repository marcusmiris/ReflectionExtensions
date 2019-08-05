using System;
using System.ComponentModel;

namespace Miris.Reflection
{
    public static partial class ReflectionSpecifications
    {

        /// <summary>
        /// NotEspecification convert a original
        /// specification with NOT logic operator
        /// </summary>
        /// <typeparam name="T">Type of element for this specificaiton</typeparam>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public sealed class NotSpecification<T> : Specification<T>
        {
            #region Members

            readonly ISpecification<T> _originalSpecification;

            #endregion

            #region Constructor

            /// <summary>
            /// Constructor for NotSpecificaiton
            /// </summary>
            /// <param name="originalSpecification">Original specification</param>
            public NotSpecification(ISpecification<T> originalSpecification)
            {
                _originalSpecification = originalSpecification ?? throw new ArgumentNullException(nameof(originalSpecification));
            }

            #endregion

            #region Override Specification methods

            /// <summary>
            /// <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.ISpecification{TEntity}"/>
            /// </summary>
            /// <returns><see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.ISpecification{TEntity}"/></returns>
            public override bool SatisfiedBy(T candidate)
            {
                return !_originalSpecification.SatisfiedBy(candidate);
            }

            #endregion
        }
    }
}
