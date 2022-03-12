using MfeGlobalConfigurations.Domain;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Infrastructure
{
    /// <summary>
    /// Global Configuration Repository is not thought for being used in production.
    /// </summary>
    public sealed class MfeGlobalConfigurationInMemoryRepository : IMfeGlobalConfigurationRepository
    {
        private static readonly Dictionary<MfeId, MfeGlobalConfiguration> GlobalConfigurations = new();

        public void Save(MfeGlobalConfiguration configuration)
        {
            GlobalConfigurations[configuration.MfeId] = configuration;
        }

        public MfeGlobalConfiguration? Search(MfeId name)
        {
            GlobalConfigurations.TryGetValue(name, out var value);
            return value;
        }

        public void Update(MfeGlobalConfiguration configuration)
        {
            GlobalConfigurations[configuration.MfeId] = configuration;
        }
    }
}
