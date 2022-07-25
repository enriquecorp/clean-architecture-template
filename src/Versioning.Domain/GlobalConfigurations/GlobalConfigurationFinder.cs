using Versioning.Domain.Shared.ValueObjects;

namespace Versioning.Domain.GlobalConfigurations
{
    public sealed class GlobalConfigurationFinder
    {
        private readonly IGlobalConfigurationRepository repository;

        public GlobalConfigurationFinder(IGlobalConfigurationRepository repository)
        {
            this.repository = repository;
        }

        // The domain services won't throw exceptions preferably!!!
        //public async Task<GlobalConfiguration> Execute(MfeId name)
        //{
        //    var configuration = await this.repository.Search(name);
        //    //if (configuration == null)
        //    //{
        //    //    throw new MfeGlobalConfigurationNotFound(name);
        //    //}

        //    return configuration;
        //}

        public async Task<GlobalConfiguration?> Find(MfeId name)
        {
            var configuration = await this.repository.Search(name);

            return configuration;
        }
    }
}
