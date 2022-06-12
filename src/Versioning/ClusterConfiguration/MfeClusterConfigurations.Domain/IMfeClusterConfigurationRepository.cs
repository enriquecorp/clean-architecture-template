using Versioning.Shared.Domain.ValueObjects;

namespace MfeClusterConfigurations.Domain
{
    public interface IMfeClusterConfigurationRepository
    {
        public Task<MfeClusterConfiguration?> Search(MfeId name, ClusterId id);
        public Task Save(MfeClusterConfiguration mfeConfiguration);
    }
}
