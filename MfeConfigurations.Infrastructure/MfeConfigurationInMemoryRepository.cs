using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MfeConfigurations.Domain;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Infrastructure
{
    public sealed class MfeConfigurationInMemoryRepository : IMfeConfigurationRepository
    {
        private static readonly Dictionary<Tuple<string, string>, MfeConfiguration> ConfigurationsByTenant = new();

        public void Save(MfeConfiguration mfeConfiguration)
        {
            ConfigurationsByTenant[Tuple.Create(mfeConfiguration.TenantId.Value, mfeConfiguration.MfeId.Value)] = mfeConfiguration;
        }

        public MfeConfiguration? Search(TenantId id, MfeId name)
        {
            var exists = ConfigurationsByTenant.TryGetValue(Tuple.Create(id.Value, name.Value), out var mfeConfiguration);
            return exists && mfeConfiguration != null ? mfeConfiguration : null;
        }
    }
}
