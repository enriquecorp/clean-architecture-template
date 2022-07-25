using Shared.Domain.ValueObjects;

namespace Versioning.Domain.Shared.ValueObjects
{
    public class MfeId : StringValueObject//, IEquatable<MfeId>
    {
        public MfeId(string value) : base(value.Trim().ToLower())
        {
        }

        //public bool Equals(MfeId? other)
        //{
        //    return this.Value.Equals(other?.Value);
        //}

        public override bool Equals(object? obj)
        {
            if (obj is MfeId nameId)
            {
                return this.Value.Equals(nameId.Value);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}
