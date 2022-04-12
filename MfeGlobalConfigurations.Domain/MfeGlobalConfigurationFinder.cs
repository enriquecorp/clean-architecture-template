using MfeGlobalConfigurations.Domain.Exceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Domain
{
    public sealed class MfeGlobalConfigurationFinder
    {
        private readonly IMfeGlobalConfigurationRepository repository;

        public MfeGlobalConfigurationFinder(IMfeGlobalConfigurationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<MfeGlobalConfiguration> Execute(MfeId name)
        {
            var configuration = await this.repository.Search(name);
            if (configuration == null)
            {
                throw new MfeGlobalConfigurationNotFound(name);
            }

            return configuration;
        }

        public async Task<MfeGlobalConfiguration?> Find(MfeId name)
        {
            var configuration = await this.repository.Search(name);

            return configuration;
        }
    }
}
