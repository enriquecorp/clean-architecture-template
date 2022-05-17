using Versioning.Shared.Domain.ValueObjects;
using Versioning.Shared.Tests.Domain.Simples;

namespace Versioning.Shared.Tests.Domain
{
    public class ClusterIdMother
    {
        public static ClusterId Create(string value)
        {
            return new ClusterId(value);
        }

        public static ClusterId Random()
        {
            return Create(UuidMother.Random());
        }
    }
}
