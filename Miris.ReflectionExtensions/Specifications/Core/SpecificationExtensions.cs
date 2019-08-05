using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static Miris.Reflection.ReflectionSpecifications;

namespace Miris.Reflection
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class SpecificationExtensions
    {
        public static bool SatisfiesAll<T>(
            this T candidate,
            IEnumerable<ISpecification<T>> specifications)
        {
            if (candidate == null) throw new ArgumentNullException(nameof(candidate));
            if (specifications == null) throw new ArgumentNullException(nameof(specifications));

            return specifications.All(spec => spec.SatisfiedBy(candidate));
        }

    }
}
