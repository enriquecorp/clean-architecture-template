using shared.domain.ValueObjects;

namespace Versioning.Shared.Domain.ValueObjects
{
    public sealed class MfeVersion : StringValueObject
    {
        // private readonly string version;

        public MfeVersion(string version) : base(version.Trim().ToLower())
        {
            //this.version = version;
        }

        public static MfeVersion CreateEmpty()
        {
            return new MfeVersion("");
        }
    }
}
