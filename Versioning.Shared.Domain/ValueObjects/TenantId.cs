using System.Text.RegularExpressions;

namespace Versioning.Shared.Domain
{
    public class TenantId
    {
        private readonly string value;

        public TenantId(string value)
        {
            this.EnsureIsValidUuid(value);
            this.EnsureIsNumber(value);
            this.value = value;
        }

        public bool IsUHTenantId => Guid.TryParse(this.value, out _);

        public bool IsBusinessUnitId => Regex.IsMatch(this.value, @"^\d+$");

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
