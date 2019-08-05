using System.ComponentModel;

namespace Miris.Reflection
{
    public static partial class ReflectionSpecifications
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public abstract class CompositeSpecification<T> : Specification<T>
        {
            public abstract ISpecification<T> LeftSpecification { get; }

            public abstract ISpecification<T> RightSpecification { get; }
        }
    }
}