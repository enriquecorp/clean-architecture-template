using System.Text.RegularExpressions;

namespace Versioning.Shared.Domain.ValueObjects
{
    public class TenantId
    {
        public string Value { get; }

        public TenantId(string value)
        {
            this.EnsureIsValidUuid(value);
            this.EnsureIsNumber(value);
            this.Value = value;
        }

        public bool IsUHTenantId => Guid.TryParse(this.Value, out _);

        public bool IsBusinessUnitId => Regex.IsMatch(this.Value, @"^\d+$");

        private void EnsureIsValidUuid(string value)
        {
            var isValid = this.IsUHTenantId;
            if (!isValid)
            {
                throw new ArgumentException($"{nameof(TenantId)} doesn't allow the value {value}");
            }
        }

        private void EnsureIsNumber(string value)
        {
            var isValid = this.IsBusinessUnitId;
            if (!isValid)
            {
                throw new ArgumentException($"{nameof(TenantId)} doesn't allow the value {value}");
            }
        }
    }
}
