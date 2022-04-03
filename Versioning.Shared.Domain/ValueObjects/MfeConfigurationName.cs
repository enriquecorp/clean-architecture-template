using shared.domain.ValueObjects;

namespace Versioning.Shared.Domain.ValueObjects
{
    public sealed class MfeConfigurationName : StringValueObject//, IEquatable<MfeConfigurationName>
    {
        public MfeConfigurationName(string value) : base(value.Trim().ToLower())
        {
        }

        public static MfeConfigurationName CreateEmpty()
        {
            return new MfeConfigurationName("");
        }

        public bool IsEmpty() { return string.IsNullOrEmpty(this.Value); }

        public override bool Equals(object? obj)
        {
            if (obj is MfeConfigurationName config)
            {
                return this.Value.Equals(config.Value);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}
