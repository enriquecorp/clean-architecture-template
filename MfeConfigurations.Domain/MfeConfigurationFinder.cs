namespace MfeConfigurations.Domain
{
    public sealed class MfeConfigurationFinder
    {
        private readonly IMfeTenantConfigurationRepository repository;

        public MfeConfigurationFinder(IMfeTenantConfigurationRepository repository)
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
