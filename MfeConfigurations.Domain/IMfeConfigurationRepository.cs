using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Domain
{
    public interface IMfeConfigurationRepository
    {
        public MfeConfiguration? Search(TenantId id, MfeId name);
        public void Save(MfeConfiguration mfeConfiguration);
    }
}
