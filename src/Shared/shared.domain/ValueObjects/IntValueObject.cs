using System.Globalization;

namespace Shared.Domain.ValueObjects
{
    public class IntValueObject : ValueObject
    {
        public int Value { get; }

        public IntValueObject(int value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Value.ToString(NumberFormatInfo.InvariantInfo);
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

            if (obj is not IntValueObject item)
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
