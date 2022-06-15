using Versioning.Shared.Domain.ValueObjects;

namespace MfeTenantConfigurations.Domain
{
    public interface ITenantConfigurationRepository
    {
        public Task<TenantConfiguration?> Search(MfeId name, TenantId id);
        public Task<List<TenantConfiguration>> SearchBatch(MfeId name, List<TenantId> tenants);// IQueryable<TenantConfiguration>??
        public Task Save(TenantConfiguration configuration);
        public Task SaveBatch(List<TenantConfiguration> configurations);
        public Task UpdateBatch(List<TenantConfiguration> configurations);
    }
}
