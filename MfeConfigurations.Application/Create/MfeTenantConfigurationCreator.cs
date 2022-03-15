using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MfeConfigurations.Domain;
using MfeConfigurations.Domain.Exceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Application.Create
{
    public sealed class MfeTenantConfigurationCreator
    {
        private readonly IMfeTenantConfigurationRepository repository;
        private readonly MfeTenantConfigurationExistsChecker mfeConfigurationExistsChecker;

        public MfeTenantConfigurationCreator(IMfeTenantConfigurationRepository repository)
        {
            this.repository = repository;
            this.mfeConfigurationExistsChecker = new MfeTenantConfigurationExistsChecker(repository);

        }
        public async Task Execute (MfeTenantConfigurationRequest configuration)
        {
            var mfeConfiguration = MfeTenantConfiguration.Create(new MfeId(configuration.MfeId), new TenantId(configuration.TenantId), MfeConfigurationName.CreateEmpty(), new ConfigurationList(new Dictionary<string, string>()));
            if (await this.mfeConfigurationExistsChecker.Exists(mfeConfiguration))
            {
                throw new MfeConfigurationAlreadyExistsException(mfeConfiguration.TenantId, mfeConfiguration.MfeId);
            }
            await this.repository.Save(mfeConfiguration);
            // $this->bus->publish(...$course->pullDomainEvents());
        }
    }
}
