using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Domain
{
    public sealed class MfeConfigurationExistsChecker
    {
        private readonly IMfeConfigurationRepository repository;

        public MfeConfigurationExistsChecker(IMfeConfigurationRepository repository)
        {
            this.repository = repository;
        }

        public bool Exists(MfeConfiguration configuration)
        {
            var mfeconfiguration = this.repository.Search(configuration.TenantId, configuration.MfeId);
            return mfeconfiguration != null;
        }
    }
}
