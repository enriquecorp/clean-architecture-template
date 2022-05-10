using Versioning.Shared.Domain.ValueObjects;

namespace MfeClusterConfigurations.Domain
{
    public interface IMfeClusterConfigurationRepository
    {
        public Task<MfeClusterConfiguration?> Search(MfeId name, TenantId id);
        //public Task<List<MfeTenantConfiguration>> SearchBatch(MfeId name, List<TenantId> tenants);// IQueryable<MfeTenantConfiguration>??
        public Task Save(MfeClusterConfiguration mfeConfiguration);
        //public Task SaveBatch(List<MfeTenantConfiguration> mfeConfiguration);
        //public Task UpdateBatch(List<MfeTenantConfiguration> configurations);
    }
}
