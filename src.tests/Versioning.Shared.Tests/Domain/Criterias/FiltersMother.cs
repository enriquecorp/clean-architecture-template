using Shared.Domain.FiltersByCriteria;

namespace Versioning.Shared.Tests.Domain.Criterias
{
    public static class FiltersMother
    {
        public static Filters Create(List<Filter> filters)
        {
            return new Filters(filters);
        }

        public static Filters CreateOne(Filter filter)
        {
            return Create(new List<Filter> { filter });
        }

        public static Filters Blank()
        {
            return Create(new List<Filter>());
        }

        public static Filters Random()
        {
            return Create(new List<Filter>
            {
                FilterMother.Random()
            });
        }
    }
}
