using Versioning.Shared.Domain.ValueObjects;

namespace MfeClusterConfigurations.Domain
{
    public interface IClusterConfigurationRepository
    {
        public Task<ClusterConfiguration?> Search(MfeId name, ClusterId id);
        public Task Save(ClusterConfiguration configuration);
    }
}
