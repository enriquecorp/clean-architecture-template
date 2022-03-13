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
        public Task<MfeTenantConfiguration?> Search(TenantId id, MfeId name);
        public Task Save(MfeTenantConfiguration mfeConfiguration);
    }
}
