using System.Text.RegularExpressions;

namespace Versioning.Domain.Shared.ValueObjects
{
    public class TenantId
    {
        public string Value { get; }

        public TenantId(string value)
        {
            this.EnsureIsUuidOrNumber(value);
            this.Value = value;
        }

        public bool IsUHTenantId => this.IsUuid(this.Value);

        public bool IsBusinessUnitId => this.IsNumber(this.Value);

        private void EnsureIsUuidOrNumber(string value)
        {
            var isValid = this.IsUuid(value) || this.IsNumber(value);
            if (!isValid)
            {
                throw new ArgumentException($"{nameof(TenantId)} doesn't allow the value {value}");
            }
        }

        private bool IsUuid(string value) => Guid.TryParse(value, out _);

        private bool IsNumber(string value) => Regex.IsMatch(value, @"^\d+$");

        public override bool Equals(object? obj)
        {
            if (obj is TenantId tenantId)
            {
                return this.Value.Equals(tenantId.Value);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}
