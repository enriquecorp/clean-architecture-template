namespace Versioning.Shared.Domain.ValueObjects
{
    public class ClusterId
    {
        public string Value { get; }

        public ClusterId(string value)
        {
            this.Value = value;
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
