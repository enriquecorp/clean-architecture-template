using Versioning.Domain.ValueObjects;

namespace Versioning.Domain.ClusterConfigurations
{
    public interface IClusterConfigurationRepository
    {
        public Task<ClusterConfiguration?> Search(MfeId name, ClusterId id);
        public Task<List<ClusterConfiguration>> SearchBatch(MfeId name, List<ClusterId> clusterIds);
        public Task Save(ClusterConfiguration configuration);
        public Task UpdateBatch(List<ClusterConfiguration> configurations);
    }
}
