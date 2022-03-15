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
        public Task<MfeTenantConfiguration?> Search(MfeId name, TenantId id);
        public Task<List<MfeTenantConfiguration>> Search(MfeId name, List<TenantId> tenants);// IQueryable<MfeTenantConfiguration>??
        public Task Save(MfeTenantConfiguration mfeConfiguration);
        public Task Update(List<MfeTenantConfiguration> configurations);
    }
}
