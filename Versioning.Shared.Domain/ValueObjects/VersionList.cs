using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Versioning.Shared.Domain.ValueObjects
{
    public sealed class VersionList
    {
        private readonly Dictionary<MfeConfigurationName, MfeVersion> versions = new();
        public MfeVersion this[MfeConfigurationName index]
        {
            get => this.versions[index];
            set => this.versions[index] = value;
        }

        public VersionList(Dictionary<string, string> versions)
        {
            foreach (var item in versions)
            {
                this[new MfeConfigurationName(item.Key)] = new MfeVersion(item.Value);
            }
        }
    }
}
