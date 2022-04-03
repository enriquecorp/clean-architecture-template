namespace MfeConfigurations.Application.Find
{
    public sealed class ConfigurationVersionResponse
    {
        public string Version { get; set; }
        public string ConfigurationName { get; set; }
        public string MfeUrl { get; set; } = string.Empty;
    }
}
