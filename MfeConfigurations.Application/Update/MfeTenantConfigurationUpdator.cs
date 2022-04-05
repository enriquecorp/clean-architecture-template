using MfeConfigurations.Domain;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Application.Update
{
    public sealed class MfeTenantConfigurationUpdator
    {
        private readonly IMfeTenantConfigurationRepository repository;

        public MfeTenantConfigurationUpdator(IMfeTenantConfigurationRepository repository)
        {
            this.repository = repository;

        }

        /// <summary>
        /// It will update the {version} for a given {configuration} for one microfrontend {name} & {tenants} list
        /// Optionally, it will set that {configuration} as active
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configuration"></param>
        /// <param name="tenants"></param>
        /// <param name="version"></param>
        /// <param name="setConfigurationActive"></param>
        /// <returns></returns>
        public async Task Execute(MfeId name, MfeConfigurationName configuration, IEnumerable<TenantId> tenants, MfeVersion version, bool setConfigurationActive)
        {
            //this.EnsureVersionsAreNotEmpty(name, versions);

            var configurations = await this.repository.SearchBatch(name, tenants.ToList());
            foreach (var c in configurations)
            {
                if (setConfigurationActive && c.ActiveConfiguration != configuration)
                {
                    c.UpdateActiveConfiguration(configuration);
                }
                var newConfigurationList = new ConfigurationList(new Dictionary<MfeConfigurationName, MfeVersion>() { { configuration, version } });
                c.UpdateConfigurations(newConfigurationList);
            }
            //if (configuration == null)
            //{
            //    configuration = MfeGlobalConfiguration.Create(name, configurations);
            //    await this.repository.Save(configuration);
            //    // $this->bus->publish(...$course->pullDomainEvents());
            //    return;
            //}
            //configuration.UpdateVersions(configurations);
            await this.repository.UpdateBatch(configurations);
            //// $this->bus->publish(...$course->pullDomainEvents());
        }
    }
}
