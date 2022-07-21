using Shared.Domain.ValueObjects;

namespace Shared.Domain.FiltersByCriteria
{
    public class OrderBy : StringValueObject
    {
        public OrderBy(string value) : base(value)
        {
        }
    }
}
