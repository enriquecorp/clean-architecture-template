using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.domain.Aggregate;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Domain
{
    public sealed class MfeGlobalConfiguration : AggregateRoot
    {
        public MfeId MfeId { get; private set; }

        public ActiveVersion ActiveVersion { get; private set; }

        public VersionList Versions { get; private set; }

        public MfeGlobalConfiguration(MfeId name, ActiveVersion active, VersionList versions)
        {
            this.MfeId = name;
            this.ActiveVersion = active;
            this.Versions = versions;
        }

        public void UpdateVersions(VersionList versions)
        {
            this.Versions = versions;
        }
    }
}
