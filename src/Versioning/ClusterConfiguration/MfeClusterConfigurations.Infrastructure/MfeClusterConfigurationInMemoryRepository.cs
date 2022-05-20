using MfeClusterConfigurations.Domain;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeClusterConfigurations.Infrastructure
{
    public sealed class MfeClusterConfigurationInMemoryRepository : IMfeClusterConfigurationRepository
    {
        private static readonly Dictionary<(string clusterId, string mfeId), MfeClusterConfiguration> ClusterConfiguration = new();

        public async Task Save(MfeClusterConfiguration mfeConfiguration)
        {
            await Task.Run(() => ClusterConfiguration[(mfeConfiguration.ClusterId.Value, mfeConfiguration.MfeId.Value)] = mfeConfiguration);
        }

        public Task SaveBatch(List<MfeClusterConfiguration> mfeConfiguration)
        {
            throw new NotImplementedException();
        }

        public Task<MfeClusterConfiguration?> Search(MfeId name, ClusterId id)
        {
            var exists = ClusterConfiguration.TryGetValue((id.Value, name.Value), out var mfeConfiguration);
            return Task.Run(() => exists && mfeConfiguration != null ? mfeConfiguration : null);
        }

        public Task<List<MfeClusterConfiguration>> SearchBatch(MfeId name, List<ClusterId> clusters)
        {
            return Task.Run(() => ClusterConfiguration.Select(t => t.Value).Where(c => c.MfeId.Value == name.Value && clusters.Contains(c.ClusterId)).ToList());
        }

        public Task UpdateBatch(List<MfeClusterConfiguration> configurations)
        {
            return Task.Run(() =>
            {
                foreach (var c in configurations)
                {
                    ClusterConfiguration[(c.ClusterId.Value, c.MfeId.Value)] = c;
                }
            });
        }
    }
}
