using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MfeConfigurations.Domain;
using MfeConfigurations.Domain.Exceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Application.Find
{

    public sealed class MfeTenantConfigurationFinder
    {
        private readonly IMfeTenantConfigurationRepository repository;

        public MfeTenantConfigurationFinder(IMfeTenantConfigurationRepository repository)
        {
            this.repository = repository;

        }

        public async Task<ConfigurationVersionResponse> Execute(TenantId tenantId, MfeId name, MfeConfigurationName? configurationName)
        {
            var configuration = await this.repository.Search(name, tenantId);
            //var configuration = configurationName==null? await this.repository.SearchActiveConfiguration(name, tenantId): await this.repository.SearchByConfigurationName(name, tenantId, configurationName);
            if (configuration == null)
            {
                throw new MfeConfigurationDoesntExistsException(tenantId, name, configurationName);
            }
            if (configuration.ActiveConfiguration.IsEmpty())
            {
                throw new NoActiveConfigurationExistsException(tenantId, name);
            }

            var version = configurationName != null ? configuration.Configurations[configurationName] : configuration.Configurations[configuration.ActiveConfiguration];
            //await this.repository.Save(configuration);
            // $this->bus->publish(...$course->pullDomainEvents());
            return new ConfigurationVersionResponse() { Version = version.Value };

            //configuration.UpdateVersions(configurations);

            //// $this->bus->publish(...$course->pullDomainEvents());
        }
    }
}
