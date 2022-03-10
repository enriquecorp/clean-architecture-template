using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MfeConfigurations.Domain;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Application.Create
{
    public sealed class MfeConfigurationCreator
    {
        private readonly IMfeConfigurationRepository repository;

        public MfeConfigurationCreator(IMfeConfigurationRepository repository)
        {
            this.repository = repository;
        }
        public void Execute (MfeConfigurationRequest configuration)
        {
            Console.WriteLine($"Execute repository or coordinate repositories");
            Console.WriteLine($"Configurations to add {configuration.Configurations.Count}");
            MfeConfiguration.Create(new TenantId(configuration.TenantId), new MfeId(configuration.MfeId));
        }
    }
}
