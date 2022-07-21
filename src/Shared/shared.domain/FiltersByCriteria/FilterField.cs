using Shared.Domain.ValueObjects;

namespace Shared.Domain.FiltersByCriteria
{
    public class FilterField : StringValueObject
    {
        public FilterField(string value) : base(value)
        {
        }
    }
}
