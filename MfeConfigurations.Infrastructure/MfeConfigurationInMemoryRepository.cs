using MfeConfigurations.Domain;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Infrastructure
{
    public sealed class MfeConfigurationInMemoryRepository : IMfeTenantConfigurationRepository
    {
        private static readonly Dictionary<(string tenantId, string mfeId), MfeTenantConfiguration> TenantConfiguration = new();

        public async Task Save(MfeTenantConfiguration mfeConfiguration)
        {
            await Task.Run(() => TenantConfiguration[(mfeConfiguration.TenantId.Value, mfeConfiguration.MfeId.Value)] = mfeConfiguration);
        }

        public Task SaveBatch(List<MfeTenantConfiguration> mfeConfiguration)
        {
            throw new NotImplementedException();
        }

        public Task<MfeTenantConfiguration?> Search(MfeId name, TenantId id)
        {
            var exists = TenantConfiguration.TryGetValue((id.Value, name.Value), out var mfeConfiguration);
            return Task.Run(() => exists && mfeConfiguration != null ? mfeConfiguration : null);
        }

        public Task<List<MfeTenantConfiguration>> SearchBatch(MfeId name, List<TenantId> tenants)
        {
            return Task.Run(() => TenantConfiguration.Select(t => t.Value).Where(t => t.MfeId.Value == name.Value && tenants.Contains(t.TenantId)).ToList());
        }

        public Task UpdateBatch(List<MfeTenantConfiguration> configurations)
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
