using MfeGlobalConfigurations.Domain;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Infrastructure
{
    /// <summary>
    /// Global Configuration Repository is not thought for being used in production.
    /// </summary>
    public sealed class MfeGlobalConfigurationInMemoryRepository : IMfeGlobalConfigurationRepository
    {
        private static readonly Dictionary<string, MfeGlobalConfiguration> GlobalConfigurations = new();

        public async Task Save(MfeGlobalConfiguration configuration)
        {
            await Task.Run(() => GlobalConfigurations[configuration.MfeId.Value] = configuration);
        }

        public async Task<MfeGlobalConfiguration?> Search(MfeId name)
        {
            return await Task.Run(() =>
            {
                _ = GlobalConfigurations.TryGetValue(name.Value, out var value);
                return value;
            });
        }

        public async Task Update(MfeGlobalConfiguration configuration)
        {
            await Task.Run(() => GlobalConfigurations[configuration.MfeId.Value] = configuration);
        }
    }
}
