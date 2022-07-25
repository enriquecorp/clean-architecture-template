using Shared.Domain.ValueObjects;

namespace Versioning.Domain.Shared.ValueObjects
{
    public sealed class ConfigurationName : StringValueObject//, IEquatable<ConfigurationName>
    {
        public ConfigurationName(string value) : base(value.Trim().ToLower())
        {
        }

        public static ConfigurationName CreateEmpty()
        {
            return new ConfigurationName("");
        }

        public bool IsEmpty() { return string.IsNullOrEmpty(this.Value); }

        public override bool Equals(object? obj)
        {
            if (obj is ConfigurationName config)
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
