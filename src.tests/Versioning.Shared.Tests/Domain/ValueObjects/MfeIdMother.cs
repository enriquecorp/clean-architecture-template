using Versioning.Domain.ValueObjects;
using Versioning.Shared.Tests.Domain.Simples;

namespace Versioning.Shared.Tests.Domain.ValueObjects
{
    public sealed class MfeIdMother
    {
        public static MfeId Create(string value)
        {
            return new MfeId(value);
        }

        public static MfeId Random()
        {
            return Create(UuidMother.Random());
        }
    }
}
