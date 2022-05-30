﻿using shared.domain.ValueObjects;

namespace Versioning.Shared.Domain.ValueObjects
{
    public class ClusterId : StringValueObject
    {

        public ClusterId(string value) : base(value.Trim().ToLower())
        {
        }

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
