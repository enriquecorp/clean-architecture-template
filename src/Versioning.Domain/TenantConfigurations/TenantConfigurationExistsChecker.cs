namespace Versioning.Domain.TenantConfigurations
{
    public sealed class TenantConfigurationExistsChecker
    {
        private readonly ITenantConfigurationRepository repository;

        public TenantConfigurationExistsChecker(ITenantConfigurationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Exists(TenantConfiguration configuration)
        {
            var configurationFound = await this.repository.Search(configuration.MfeId, configuration.TenantId);
            return configurationFound != null;
        }
    }
}
