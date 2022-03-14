using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MfeConfigurations.Domain;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Application.UpdateActiveConfiguration
{
    public sealed class MfeActiveConfigurationUpdator
    {
        private readonly IMfeTenantConfigurationRepository repository;

        public MfeActiveConfigurationUpdator(IMfeTenantConfigurationRepository repository)
        {
            this.repository = repository;

        }

        public async Task Execute(MfeId name, MfeConfigurationName activeConfiguration, IEnumerable<TenantId> tenants)
        {
            //this.EnsureVersionsAreNotEmpty(name, versions);

            var configurations = await this.repository.Search(name, tenants.ToList());
            foreach (var c in configurations)
            {
                c.UpdateActiveConfiguration(activeConfiguration);
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
