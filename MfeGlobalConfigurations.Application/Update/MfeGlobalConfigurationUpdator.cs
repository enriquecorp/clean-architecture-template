using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MfeGlobalConfigurations.Domain;
using MfeGlobalConfigurations.Domain.Exceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Application.Update
{
    public sealed class MfeGlobalConfigurationUpdator
    {
        private readonly IMfeGlobalConfigurationRepository repository;

        public MfeGlobalConfigurationUpdator(IMfeGlobalConfigurationRepository repository)
        {
            this.repository = repository;

        }

        public async Task Execute(MfeId name, ConfigurationList configurations)
        {
           

            var configuration = await this.repository.Search(name);
            if (configuration == null)
            {
                configuration = MfeGlobalConfiguration.Create(name, configurations);
                await this.repository.Save(configuration);
                // $this->bus->publish(...$course->pullDomainEvents());
                return;
            }
            configuration.UpdateConfigurations(configurations);
            await this.repository.Update(configuration);
            // $this->bus->publish(...$course->pullDomainEvents());
        }

        private void EnsureVersionsAreNotEmpty(MfeId name, ConfigurationList versions)
        {
            if (versions==null || versions.Length < 0)
            {
                throw new MfeVersionsAreEmpty(name);
            }
        }
    }
}
