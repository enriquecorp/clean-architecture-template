using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task Execute(MfeId name, MfeConfigurationName configuration, IEnumerable<TenantId> tenants, MfeVersion version, bool setConfigurationActive)
        {
            //this.EnsureVersionsAreNotEmpty(name, versions);

            var configurations = await this.repository.Search(name, tenants.ToList());
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
            await this.repository.Update(configurations);
            //// $this->bus->publish(...$course->pullDomainEvents());
        }
    }
}
