using System;
using shared.domain.ValueObjects;

namespace Versioning.Shared.Domain.ValueObjects
{
    public class MfeId : StringValueObject, IEquatable<MfeId>
    {
        public MfeId(string value) : base(value.Trim().ToLower())
        {
        }

        public bool Equals(MfeId? other)
        {
            return this.Value.Equals(other?.Value);
        }
    }
}
