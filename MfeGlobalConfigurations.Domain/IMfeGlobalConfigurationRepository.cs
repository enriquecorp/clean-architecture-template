using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Domain
{
    public interface IMfeGlobalConfigurationRepository
    {
        public MfeGlobalConfiguration? Search(MfeId name);
        public void Save(MfeGlobalConfiguration globalConfiguration);
    }
}
