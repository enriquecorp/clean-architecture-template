using Versioning.Domain.Shared.ValueObjects;
using Versioning.Shared.Tests.Domain.Simples;

namespace Versioning.Shared.Tests.Domain.ValueObjects
{
    public sealed class VersionUrlMother
    {
        public static VersionUrl Create(string value)
        {
            return new VersionUrl(value);
        }


        public static VersionUrl Random()
        {
            return Create(UrlMother.Random());
        }

        public static VersionUrl CreateEmpty()
        {
            return new VersionUrl("");
        }
    }
}
