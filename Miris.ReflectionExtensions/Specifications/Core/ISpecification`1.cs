using System.ComponentModel;

namespace Miris.Reflection
{
    public static partial class ReflectionSpecifications
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public interface ISpecification<in T>
        {
            bool SatisfiedBy(T candidate);
        }
    }
}