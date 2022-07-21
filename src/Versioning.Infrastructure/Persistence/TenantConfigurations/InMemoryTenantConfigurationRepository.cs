using Versioning.Domain.TenantConfigurations;
using Versioning.Domain.ValueObjects;

namespace Versioning.Infrastructure.Persistence.TenantConfigurations
{
    public sealed class InMemoryTenantConfigurationRepository : ITenantConfigurationRepository
    {
        private static readonly Dictionary<(string tenantId, string mfeId), TenantConfiguration> TenantConfiguration = new();

        public async Task Save(TenantConfiguration configuration)
        {
            await Task.Run(() => TenantConfiguration[(configuration.TenantId.Value, configuration.MfeId.Value)] = configuration);
        }

        public Task SaveBatch(List<TenantConfiguration> configuration)
        {
            throw new NotImplementedException();
        }

        public Task<TenantConfiguration?> Search(MfeId name, TenantId id)
        {
            var exists = TenantConfiguration.TryGetValue((id.Value, name.Value), out var configuration);
            return Task.Run(() => exists && configuration != null ? configuration : null);
        }

        public Task<List<TenantConfiguration>> SearchBatch(MfeId name, List<TenantId> tenants)
        {
            return Task.Run(() => TenantConfiguration.Select(t => t.Value).Where(t => t.MfeId.Value == name.Value && tenants.Contains(t.TenantId)).ToList());
        }

        public Task UpdateBatch(List<TenantConfiguration> configurations)
        {
            return Task.Run(() =>
            {
                foreach (var c in configurations)
                {
                    TenantConfiguration[(c.TenantId.Value, c.MfeId.Value)] = c;
                }
            });
        }
    }
}
