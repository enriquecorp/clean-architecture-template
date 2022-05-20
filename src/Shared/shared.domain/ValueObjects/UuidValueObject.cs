namespace shared.domain.ValueObjects
{
    public class UuidValueObject : ValueObject
    {
        public string Value { get; }
        public Guid ValueUuid { get { Guid.TryParse(this.Value, out var id); return id; } }
        public UuidValueObject(string value)
        {
            this.EnsureIsValidUuid(value);
            this.Value = value;
        }

        private bool IsGuidId(string value) => Guid.TryParse(value, out _);

        private void EnsureIsValidUuid(string value)
        {
            var isValid = this.IsGuidId(value);
            if (!isValid)
            {
                throw new ArgumentException($"{nameof(UuidValueObject)} doesn't allow the value {value}");
            }
        }


        public override string ToString()
        {
            return this.Value;
        }

        public static UuidValueObject Random()
        {
            return new UuidValueObject(Guid.NewGuid().ToString());
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Value;
        }

        public override bool Equals(object? obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (obj is not UuidValueObject item)
            {
                return false;
            }

            return this.Value == item.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Value);
        }
    }
}
