using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MfeGlobalConfigurations.Domain;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Application.Update
{
    public sealed class MfeGlobalConfigurationUpdator
    {
        private readonly IMfeGlobalConfigurationRepository repository;
        private readonly MfeGlobalConfigurationFinder mfeGlobalConfigurationFinder;

        public MfeGlobalConfigurationUpdator(IMfeGlobalConfigurationRepository repository)
        {
            this.repository = repository;
            this.mfeGlobalConfigurationFinder = new MfeGlobalConfigurationFinder(repository);

        }

        public void Execute(MfeId name, VersionList versions)
        {
            var configuration = this.mfeGlobalConfigurationFinder.Execute(name);

            //var mfeConfiguration = MfeGlobalConfiguration.Create(new TenantId(configuration.TenantId), new MfeId(configuration.MfeId));
            //if (this.mfeConfigurationExistsChecker.Exists(mfeConfiguration))
            //{
            //    throw new MfeConfigurationAlreadyExistsException(mfeConfiguration.TenantId, mfeConfiguration.MfeId);
            //}
            //this.repository.Save(mfeConfiguration);
            //// $this->bus->publish(...$course->pullDomainEvents());
        }
    }
}
