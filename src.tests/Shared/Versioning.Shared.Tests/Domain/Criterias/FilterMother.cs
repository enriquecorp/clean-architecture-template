using System.Collections.Generic;
using shared.domain.FiltersByCriteria;

namespace Versioning.Shared.Tests.Domain.Criterias
{
    public static class FilterMother
    {
        public static Filter Create(FilterField field, FilterOperator @operator, FilterValue value)
        {
            return new Filter(field, @operator, value);
        }

        public static Filter FromValues(Dictionary<string, string> values)
        {
            return Filter.FromValues(values);
        }

        public static Filter Random()
        {
            return Create(FilterFieldMother.Random(), FilterOperatorMother.Random(), FilterValueMother.Random());
        }
    }
}
