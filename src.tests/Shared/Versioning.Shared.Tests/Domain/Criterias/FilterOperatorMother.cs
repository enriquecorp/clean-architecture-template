using shared.domain.FiltersByCriteria;

namespace Versioning.Shared.Tests.Domain.Criterias
{
    public static class FilterOperatorMother
    {
        public static FilterOperator Random()
        {
            var values = Enum.GetValues(typeof(FilterOperator));
            var random = new Random();
            return (FilterOperator)values.GetValue(random.Next(values.Length));
        }
    }
}
