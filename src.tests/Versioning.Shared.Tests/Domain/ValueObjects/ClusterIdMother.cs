using Versioning.Shared.Tests.Domain.Simples;
using Versioning.Domain.Shared.ValueObjects;

namespace Versioning.Shared.Tests.Domain.ValueObjects
{
    public class ClusterIdMother
    {
        public static ClusterId Create(string value)
        {
            return new ClusterId(value);
        }

        public static ClusterId Random()
        {
            return Create(WordMother.Random());
        }
    }
}
