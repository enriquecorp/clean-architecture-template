using Shared.Domain.ValueObjects;

namespace Shared.Domain.FiltersByCriteria
{
    public class FilterValue : StringValueObject
    {
        public FilterValue(string value) : base(value)
        {
        }
    }
}
