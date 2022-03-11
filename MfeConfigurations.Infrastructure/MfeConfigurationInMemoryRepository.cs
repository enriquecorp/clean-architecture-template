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
        private static readonly Dictionary<Tuple<string, string>, MfeTenantConfiguration> ConfigurationsByTenant = new();

        public void Save(MfeTenantConfiguration mfeConfiguration)
        {
            ConfigurationsByTenant[Tuple.Create(mfeConfiguration.TenantId.Value, mfeConfiguration.MfeId.Value)] = mfeConfiguration;
        }

        public MfeTenantConfiguration? Search(TenantId id, MfeId name)
        {
            var exists = ConfigurationsByTenant.TryGetValue(Tuple.Create(id.Value, name.Value), out var mfeConfiguration);
            return exists && mfeConfiguration != null ? mfeConfiguration : null;
        }
    }
}
