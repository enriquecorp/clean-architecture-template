using Shared.Domain.FiltersByCriteria;

namespace Versioning.Shared.Tests.Domain.Criterias
{
    public static class FilterOperatorMother
    {
        public static FilterOperator Random()
        {
            var values = Enum.GetValues(typeof(FilterOperator));
            var random = new Random();
            var selected = values.GetValue(random.Next(values.Length)) ?? FilterOperator.EQUAL;
            return (FilterOperator)selected;
        }
    }
}
