using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MfeConfigurations.Domain;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Infrastructure
{
    public sealed class MfeConfigurationInMemoryRepository : IMfeConfigurationRepository
    {
        public MfeConfiguration? Search(TenantId id, MfeId name)
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}
