using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Domain
{
    public sealed class MfeTenantConfigurationExistsChecker
    {
        private readonly IMfeTenantConfigurationRepository repository;

        public MfeTenantConfigurationExistsChecker(IMfeTenantConfigurationRepository repository)
        {
            this.repository = repository;
        }

        public bool Exists(MfeTenantConfiguration configuration)
        {
            var mfeconfiguration = this.repository.Search(configuration.TenantId, configuration.MfeId);
            return mfeconfiguration != null;
        }
    }
}
