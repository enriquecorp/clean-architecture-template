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
        public void Execute (MfeConfigurationRequest configuration)
        {
            var mfeConfiguration = MfeTenantConfiguration.Create(new MfeId(configuration.MfeId), new TenantId(configuration.TenantId), new MfeConfigurationName(""), new ConfigurationList(new Dictionary<string, string>()));
            if (this.mfeConfigurationExistsChecker.Exists(mfeConfiguration))
            {
                throw new MfeConfigurationAlreadyExistsException(mfeConfiguration.TenantId, mfeConfiguration.MfeId);
            }
            this.repository.Save(mfeConfiguration);
            // $this->bus->publish(...$course->pullDomainEvents());
        }
    }
}
