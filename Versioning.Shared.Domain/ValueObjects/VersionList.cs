using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Versioning.Shared.Domain.ValueObjects
{
    public sealed class VersionList : Dictionary<MfeConfigurationName, MfeVersion>
    {
        private readonly string[] supportedConfigurations = { "current", "previous", "preview" };
        //private readonly Dictionary<MfeConfigurationName, MfeVersion> versions = new();

        public int Length => this.Count;

        //public MfeVersion this[MfeConfigurationName index]
        //{
        //    get => this.versions[index];
        //    set => this.versions[index] = value;
        //}

        public VersionList(Dictionary<string, string> versions)
        {
            foreach (var configuration in this.supportedConfigurations)
            {
                versions.TryGetValue(configuration, out var version);
                this[new MfeConfigurationName(configuration)] = version != null ? new MfeVersion(version) : new MfeVersion("");
                //if (version != null)
                //{
                //    this[new MfeConfigurationName(configuration)] = new MfeVersion(version);
                //}
                //else
                //{
                //    this[new MfeConfigurationName(configuration)] = new MfeVersion("");
                //}
            }
        }

        public MfeConfigurationName GetFirstConfigurationName() => this.First().Key;
    }
}
