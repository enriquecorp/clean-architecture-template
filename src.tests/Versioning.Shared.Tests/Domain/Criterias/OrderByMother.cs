using Shared.Domain.FiltersByCriteria;
using Versioning.Shared.Tests.Domain.Simples;

namespace Versioning.Shared.Tests.Domain.Criterias
{
    public static class OrderByMother
    {
        public static OrderBy Create(string? fieldName = null)
        {
            return new OrderBy(fieldName ?? WordMother.Random());
        }

        public static OrderBy Random()
        {
            return Create(WordMother.Random());
        }
    }
}
