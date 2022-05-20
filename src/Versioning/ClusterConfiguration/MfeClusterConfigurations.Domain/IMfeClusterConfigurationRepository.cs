using Versioning.Shared.Domain.ValueObjects;

namespace MfeClusterConfigurations.Domain
{
    public interface IMfeClusterConfigurationRepository
    {
        public Task<MfeClusterConfiguration?> Search(MfeId name, ClusterId id);
        //public Task<List<MfeTenantConfiguration>> SearchBatch(MfeId name, List<ClusterId> tenants);// IQueryable<MfeTenantConfiguration>??
        public Task Save(MfeClusterConfiguration mfeConfiguration);
        //public Task SaveBatch(List<MfeTenantConfiguration> mfeConfiguration);
        //public Task UpdateBatch(List<MfeTenantConfiguration> configurations);
    }
}
