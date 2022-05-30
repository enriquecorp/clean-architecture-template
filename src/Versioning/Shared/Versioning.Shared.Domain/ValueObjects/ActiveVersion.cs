using shared.domain.ValueObjects;

namespace Versioning.Shared.Domain.ValueObjects
{
    public class ActiveVersion : StringValueObject
    {
        public ActiveVersion(string value) : base(value.Trim().ToLower())
        {
        }
    }
}
