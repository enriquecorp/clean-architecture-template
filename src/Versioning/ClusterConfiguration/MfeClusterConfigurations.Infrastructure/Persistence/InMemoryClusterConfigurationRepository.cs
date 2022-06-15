using MfeClusterConfigurations.Domain;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeClusterConfigurations.Infrastructure.Persistence
{
    public sealed class InMemoryClusterConfigurationRepository : IClusterConfigurationRepository
    {
        private static readonly Dictionary<(string clusterId, string mfeId), ClusterConfiguration> ClusterConfiguration = new();

        public async Task Save(ClusterConfiguration configuration)
        {
            await Task.Run(() => ClusterConfiguration[(configuration.ClusterId.Value, configuration.MfeId.Value)] = configuration);
        }

        public Task SaveBatch(List<ClusterConfiguration> configurations)
        {
            throw new NotImplementedException();
        }

        public Task<ClusterConfiguration?> Search(MfeId name, ClusterId id)
        {
            var exists = ClusterConfiguration.TryGetValue((id.Value, name.Value), out var configuration);
            return Task.Run(() => exists && configuration != null ? configuration : null);
        }

        public Task<List<ClusterConfiguration>> SearchBatch(MfeId name, List<ClusterId> clusters)
        {
            return Task.Run(() => ClusterConfiguration.Select(t => t.Value).Where(c => c.MfeId.Value == name.Value && clusters.Contains(c.ClusterId)).ToList());
        }

        public Task UpdateBatch(List<ClusterConfiguration> configurations)
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
