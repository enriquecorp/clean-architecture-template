using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.domain.ValueObjects
{
    public abstract class UuidValueObject
    {
        public string Value { get; }
        public Guid ValueUuid { get { Guid.TryParse(this.Value, out var id); return id; } }
        public UuidValueObject(string value)
        {
            this.Value = value;
        }

        private bool IsGuidId => Guid.TryParse(this.Value, out _);

        private void EnsureIsValidUuid(string value)
        {
            var isValid = this.IsGuidId;
            if (!isValid)
            {
                throw new ArgumentException($"{nameof(UuidValueObject)} doesn't allow the value {value}");
            }
        }
    }
}
