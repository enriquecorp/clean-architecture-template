using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
