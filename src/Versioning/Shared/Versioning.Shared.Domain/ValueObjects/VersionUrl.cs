using shared.domain.ValueObjects;

namespace Versioning.Shared.Domain.ValueObjects
{
    public sealed class VersionUrl : StringValueObject
    {
        // private readonly string versionUrl;

        public VersionUrl(string versionUrl) : base(versionUrl.Trim().ToLower())
        {
            //this.versionUrl = versionUrl;
        }

        public static VersionUrl CreateEmpty()
        {
            return new VersionUrl("");
        }
    }
}
