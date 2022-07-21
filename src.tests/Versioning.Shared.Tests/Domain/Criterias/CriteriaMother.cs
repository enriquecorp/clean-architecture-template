using Shared.Domain.FiltersByCriteria;
using Versioning.Shared.Tests.Domain.Simples;

namespace Versioning.Shared.Tests.Domain.Criterias
{
    public static class CriteriaMother
    {
        public static Criteria Create(Filters filters, Order order, int? limit = null, int? offset = null)
        {
            return new Criteria(filters, order, offset, limit);
        }

        public static Criteria Empty()
        {
            return Create(FiltersMother.Blank(), Order.None());
        }

        public static Criteria Random()
        {
            return Create(FiltersMother.Random(), OrderMother.Random(), IntegerMother.Random(), IntegerMother.Random());
        }
    }
}
