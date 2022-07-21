using Versioning.Domain.Constants;

namespace Versioning.Domain.ValueObjects
{
    public sealed class ConfigurationList : Dictionary<ConfigurationName, VersionUrl>
    {
        public int Length => this.Count;

        public ConfigurationList(Dictionary<string, string> configurations)
        {
            foreach (var configuration in Configuration.SupportedConfigurations)
            {
                configurations.TryGetValue(configuration, out var versionUrl);
                this[new ConfigurationName(configuration)] = versionUrl != null ? new VersionUrl(versionUrl) : VersionUrl.CreateEmpty();
            }
        }

        public ConfigurationList(Dictionary<ConfigurationName, VersionUrl> configurations)
        {
            foreach (var configuration in Configuration.SupportedConfigurations)
            {
                var c = new ConfigurationName(configuration);
                configurations.TryGetValue(c, out var versionUrl);
                this[c] = versionUrl ?? new VersionUrl("");
            }
        }

        public ConfigurationName GetFirstConfigurationName() => this.First().Key;

        public string Value
        {
            get
            {
                string dictionaryString = "{";
                foreach (KeyValuePair<ConfigurationName, VersionUrl> configurations in this)
                {
                    dictionaryString += configurations.Key.Value + " : " + configurations.Value + ", ";
                }
                return dictionaryString.TrimEnd(',', ' ') + "}";
            }
        }
    }
}
