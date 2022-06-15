using MfeGlobalConfigurations.Domain;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Infrastructure.Persistence
{
    /// <summary>
    /// Global Configuration Repository is not thought for being used in production.
    /// </summary>
    public sealed class InMemoryGlobalConfigurationRepository : IGlobalConfigurationRepository
    {
        private static readonly Dictionary<string, GlobalConfiguration> GlobalConfigurations = new();

        public async Task Save(GlobalConfiguration configuration)
        {
            await Task.Run(() => GlobalConfigurations[configuration.MfeId.Value] = configuration);
        }

        public async Task<GlobalConfiguration?> Search(MfeId name)
        {
            return await Task.Run(() =>
            {
                _ = GlobalConfigurations.TryGetValue(name.Value, out var value);
                return value;
            });
        }

        public async Task Update(GlobalConfiguration configuration)
        {
            await Task.Run(() => GlobalConfigurations[configuration.MfeId.Value] = configuration);
        }
    }
}
