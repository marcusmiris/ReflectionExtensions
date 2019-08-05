using System;
using System.ComponentModel;

namespace Miris.Reflection
{
    public static partial class ReflectionSpecifications
    {

        [EditorBrowsable(EditorBrowsableState.Never)]
        public class AndSpecification<T> : CompositeSpecification<T>
        {

            private readonly ISpecification<T> _leftSpecification;
            private readonly ISpecification<T> _rightSpecification;

            public override ISpecification<T> LeftSpecification
            {
                get { return _leftSpecification; }
            }

            public override ISpecification<T> RightSpecification
            {
                get
                {
                    return _rightSpecification;
                }
            }

            public AndSpecification(ISpecification<T> left, ISpecification<T> right)
            {
                if (left == null) throw new ArgumentNullException("left");
                if (right == null) throw new ArgumentNullException("right");

                _leftSpecification = left;
                _rightSpecification = right;
            }


            public override bool SatisfiedBy(T candidate)
            {
                var left = _leftSpecification.SatisfiedBy(candidate);
                var right = _rightSpecification.SatisfiedBy(candidate);

                return left && right;
            }
        }

    }
    
}
