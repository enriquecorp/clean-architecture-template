using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.domain.Aggregate;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Domain
{
    public sealed class MfeConfiguration: AggregateRoot
    {
        private readonly TenantId id;
        private readonly MfeId name;

        public MfeConfiguration(TenantId id, MfeId name)
        {
            this.id = id;
            this.name = name;
        }

        public static MfeConfiguration Create(TenantId id, MfeId name)
        {
            var configuration = new MfeConfiguration(id, name);
            //configuration.Record(
            //new MfeConfigurationDomainEvent(
            //    id.Value, name.Value));

            return configuration;
        }
    }
}
