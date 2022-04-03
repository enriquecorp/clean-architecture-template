using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Domain
{
    public interface IMfeTenantConfigurationRepository
    {
        public Task<MfeTenantConfiguration?> Search(MfeId name, TenantId id);
        public Task<List<MfeTenantConfiguration>> SearchBatch(MfeId name, List<TenantId> tenants);// IQueryable<MfeTenantConfiguration>??
        public Task Save(MfeTenantConfiguration mfeConfiguration);
        public Task SaveBatch(List<MfeTenantConfiguration> mfeConfiguration);
        public Task UpdateBatch(List<MfeTenantConfiguration> configurations);
    }
}
