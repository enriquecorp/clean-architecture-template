using Shared.Domain.ValueObjects;

namespace Versioning.Domain.Shared.ValueObjects
{
    public class ActiveVersion : StringValueObject
    {
        public ActiveVersion(string value) : base(value.Trim().ToLower())
        {
        }
    }
}
