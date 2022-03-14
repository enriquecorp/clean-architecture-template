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
            throw new NotImplementedException();
        }
    }
}
