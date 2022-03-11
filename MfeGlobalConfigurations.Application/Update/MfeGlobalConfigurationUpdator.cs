using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MfeConfigurations.Domain;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Application.Update
{
    public sealed class MfeGlobalConfigurationUpdator
    {
        private readonly IMfeGlobalConfigurationRepository repository;
        //private readonly MfeTenantConfigurationExistsChecker mfeConfigurationExistsChecker;

        public MfeGlobalConfigurationUpdator(IMfeGlobalConfigurationRepository repository)
        {
            this.repository = repository;
            // this.mfeConfigurationExistsChecker = new MfeTenantConfigurationExistsChecker(repository);

        }

        public void Execute(MfeGlobalConfigurationRequest configuration)
        {
            //var mfeConfiguration = MfeConfiguration.Create(new TenantId(configuration.TenantId), new MfeId(configuration.MfeId));
            //if (this.mfeConfigurationExistsChecker.Exists(mfeConfiguration))
            //{
            //    throw new MfeConfigurationAlreadyExistsException(mfeConfiguration.TenantId, mfeConfiguration.MfeId);
            //}
            //this.repository.Save(mfeConfiguration);
            //// $this->bus->publish(...$course->pullDomainEvents());
        }
    }
}
