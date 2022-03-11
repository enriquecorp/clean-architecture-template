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
    public sealed class MfeConfigurationCreator
    {
        private readonly IMfeTenantConfigurationRepository repository;
        private readonly MfeTenantConfigurationExistsChecker mfeConfigurationExistsChecker;

        public MfeConfigurationCreator(IMfeTenantConfigurationRepository repository)
        {
            this.repository = repository;
            this.mfeConfigurationExistsChecker = new MfeTenantConfigurationExistsChecker(repository);

        }
        public void Execute (MfeConfigurationRequest configuration)
        {
            Console.WriteLine($"Execute repository or coordinate repositories");
            Console.WriteLine($"Configurations to add {configuration.Configurations.Count}");
            var mfeConfiguration = MfeTenantConfiguration.Create(new TenantId(configuration.TenantId), new MfeId(configuration.MfeId));
            if (this.mfeConfigurationExistsChecker.Exists(mfeConfiguration))
            {
                throw new MfeConfigurationAlreadyExistsException(mfeConfiguration.TenantId, mfeConfiguration.MfeId);
            }
            this.repository.Save(mfeConfiguration);
            // $this->bus->publish(...$course->pullDomainEvents());
        }
    }
}
