using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.domain.ValueObjects;

namespace Versioning.Shared.Domain.ValueObjects
{
    public sealed class MfeConfigurationName : StringValueObject//, IEquatable<MfeConfigurationName>
    {
        public MfeConfigurationName(string value) : base(value.Trim().ToLower())
        {
        }

        //public bool Equals(MfeConfigurationName? other)
        //{
        //    return this.Value.Equals(other?.Value);
        //}

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
