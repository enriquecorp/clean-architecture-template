using shared.domain.FiltersByCriteria;
using Versioning.Shared.Tests.Domain.Simples;

namespace Versioning.Shared.Tests.Domain.Criterias
{
    public static class FilterFieldMother
    {
        public static FilterField Create(string fieldName)
        {
            return new FilterField(fieldName);
        }

        public static FilterField Random()
        {
            return new FilterField(WordMother.Random());
        }
    }
}
