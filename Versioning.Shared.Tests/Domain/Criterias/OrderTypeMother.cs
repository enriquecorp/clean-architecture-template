using System;
using shared.domain.FiltersByCriteria;

namespace Versioning.Shared.Tests.Domain.Criterias
{
    public static class OrderTypeMother
    {
        public static OrderType Random()
        {
            var values = Enum.GetValues(typeof(OrderType));
            var random = new Random();
            return (OrderType)values.GetValue(random.Next(values.Length));
        }
    }
}
