using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Domain
{
    public interface IMfeTenantConfigurationRepository
    {
        public MfeTenantConfiguration? Search(TenantId id, MfeId name);
        public void Save(MfeTenantConfiguration mfeConfiguration);
    }
}
