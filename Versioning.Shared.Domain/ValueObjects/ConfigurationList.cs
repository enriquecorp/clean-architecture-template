using Versioning.Shared.Domain.Constants;

namespace Versioning.Shared.Domain.ValueObjects
{
    public sealed class ConfigurationList : Dictionary<MfeConfigurationName, VersionUrl>
    {
        public int Length => this.Count;

        public ConfigurationList(Dictionary<string, string> configurations)
        {
            foreach (var configuration in Configuration.SupportedConfigurations)
            {
                configurations.TryGetValue(configuration, out var versionUrl);
                this[new MfeConfigurationName(configuration)] = versionUrl != null ? new VersionUrl(versionUrl) : VersionUrl.CreateEmpty();
            }
        }

        public ConfigurationList(Dictionary<MfeConfigurationName, VersionUrl> configurations)
        {
            foreach (var configuration in Configuration.SupportedConfigurations)
            {
                var c = new MfeConfigurationName(configuration);
                configurations.TryGetValue(c, out var versionUrl);
                this[c] = versionUrl ?? new VersionUrl("");
            }
        }

        public MfeConfigurationName GetFirstConfigurationName() => this.First().Key;

        public string Value
        {
            get
            {
                string dictionaryString = "{";
                foreach (KeyValuePair<MfeConfigurationName, VersionUrl> configurations in this)
                {
                    dictionaryString += configurations.Key.Value + " : " + configurations.Value + ", ";
                }
                return dictionaryString.TrimEnd(',', ' ') + "}";
            }
        }
    }
}
