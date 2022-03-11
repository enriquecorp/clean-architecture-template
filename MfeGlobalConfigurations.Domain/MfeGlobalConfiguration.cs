using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Domain
{
    public sealed class MfeGlobalConfiguration
    {
        public MfeId MfeId { get; private set; }

        public MfeGlobalConfiguration(MfeId name)
        {
            this.MfeId = name;
        }
    }
}
