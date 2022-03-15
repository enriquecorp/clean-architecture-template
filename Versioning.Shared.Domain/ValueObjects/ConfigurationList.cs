using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versioning.Shared.Domain.Constants;

namespace Versioning.Shared.Domain.ValueObjects
{
    public sealed class ConfigurationList : Dictionary<MfeConfigurationName, MfeVersion>
    {
        //private readonly Dictionary<MfeConfigurationName, MfeVersion> versions = new();

        public int Length => this.Count;

        //public MfeVersion this[MfeConfigurationName index]
        //{
        //    get => this.versions[index];
        //    set => this.versions[index] = value;
        //}

        public ConfigurationList(Dictionary<string, string> configurations)
        {
            foreach (var configuration in Configuration.SupportedConfigurations)
            {
                configurations.TryGetValue(configuration, out var version);
                this[new MfeConfigurationName(configuration)] = version != null ? new MfeVersion(version) : MfeVersion.CreateEmpty();
            }
        }

        public ConfigurationList(Dictionary<MfeConfigurationName, MfeVersion> configurations)
        {
            foreach (var configuration in Configuration.SupportedConfigurations)
            {
                var c = new MfeConfigurationName(configuration);
                configurations.TryGetValue(c, out var version);
                this[c] = version ?? new MfeVersion("");
            }
        }

        public MfeConfigurationName GetFirstConfigurationName() => this.First().Key;
    }
}
