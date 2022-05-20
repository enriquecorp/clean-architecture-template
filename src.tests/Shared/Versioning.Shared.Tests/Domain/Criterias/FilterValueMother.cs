using shared.domain.FiltersByCriteria;
using Versioning.Shared.Tests.Domain.Simples;

namespace Versioning.Shared.Tests.Domain.Criterias
{
    public static class FilterValueMother
    {
        public static FilterValue Create(string value)
        {
            return new FilterValue(value);
        }

        public static FilterValue Random()
        {
            return Create(WordMother.Random());
        }
    }
}
