namespace MfeTenantConfigurations.Domain
{
    public sealed class MfeTenantConfigurationFinder
    {
        private readonly IMfeTenantConfigurationRepository repository;

        public MfeTenantConfigurationFinder(IMfeTenantConfigurationRepository repository)
        {
            this.repository = repository;
        }

        //public MfeConfiguration Execute(TenantId id, MfeId name)
        //{
        //    var configuration = this.repository.Search(id, name);
        //    if (configuration == null)
        //        throw new MfeConfigurationNotFound(id, name);
        //    return configuration;
        //}
    }
}
