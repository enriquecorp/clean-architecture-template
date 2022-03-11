using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.domain.Aggregate;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Domain
{
    public sealed class MfeTenantConfiguration: AggregateRoot
    {
        public TenantId TenantId { get; private set; }
        public MfeId MfeId { get; private set; }

        public MfeTenantConfiguration(TenantId id, MfeId name)
        {
            this.TenantId = id;
            this.MfeId = name;
        }

        public static MfeTenantConfiguration Create(TenantId id, MfeId name)
        {
            var configuration = new MfeTenantConfiguration(id, name);

            //configuration.Record(
            //new MfeConfigurationDomainEvent(
            //    id.Value, name.Value));

            return configuration;
        }
    }
}
