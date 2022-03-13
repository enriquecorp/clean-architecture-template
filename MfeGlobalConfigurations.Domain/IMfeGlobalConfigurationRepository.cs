using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Domain
{
    public interface IMfeGlobalConfigurationRepository
    {
        public Task<MfeGlobalConfiguration?> Search(MfeId name);
        public Task Save(MfeGlobalConfiguration configuration);
        Task Update(MfeGlobalConfiguration configuration);
    }
}
