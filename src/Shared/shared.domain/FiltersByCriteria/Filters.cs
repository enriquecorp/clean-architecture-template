namespace Shared.Domain.FiltersByCriteria
{
    public class Filters
    {
        public List<Filter> Values { get; }

        public Filters(List<Filter> filters)
        {
            this.Values = filters;
        }

        public static Filters? FromValues(List<Dictionary<string, string>> filters)
        {
            if (filters == null)
            {
                return null;
            }

            return new Filters(filters.Select(Filter.FromValues).ToList());
        }
    }
}
