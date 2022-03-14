using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MfeConfigurations.Domain;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Infrastructure
{
    public sealed class MfeConfigurationInMemoryRepository : IMfeTenantConfigurationRepository
    {
        private static readonly Dictionary<Tuple<string, string>, MfeTenantConfiguration> TenantConfiguration = new();

        public async Task Save(MfeTenantConfiguration mfeConfiguration)
        {
            await Task.Run(() => TenantConfiguration[Tuple.Create(mfeConfiguration.TenantId.Value, mfeConfiguration.MfeId.Value)] = mfeConfiguration);
        }

        public Task<MfeTenantConfiguration?> Search(MfeId name, TenantId id)
        {
            var exists = TenantConfiguration.TryGetValue(Tuple.Create(id.Value, name.Value), out var mfeConfiguration);
            return Task.Run(() => exists && mfeConfiguration != null ? mfeConfiguration : null);
        }

        public Task<List<MfeTenantConfiguration>> Search(MfeId name, List<TenantId> tenants)
        {
            return Task.Run(() => TenantConfiguration.Select(t => t.Value).Where(t => t.MfeId == name && tenants.Contains(t.TenantId)).ToList());
        }

        public Task Update(List<MfeTenantConfiguration> configurations)
        {
            return Task.Run(() =>
            {
                foreach (var c in configurations)
                {
                    TenantConfiguration[Tuple.Create(c.TenantId.Value, c.MfeId.Value)] = c;
                }
            });
        }
    }
}
