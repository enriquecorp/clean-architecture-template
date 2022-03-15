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

        public async Task<bool> Exists(MfeTenantConfiguration configuration)
        {
            var mfeconfiguration = await this.repository.Search(configuration.MfeId, configuration.TenantId);
            return mfeconfiguration != null;
        }
    }
}
