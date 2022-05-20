namespace MfeTenantConfigurations.Application.Find
{
    public sealed class ConfigurationVersionResponse
    {
        public string VersionUrl { get; set; }
        public string ConfigurationName { get; set; }
        public string MfeUrl { get; set; } = string.Empty;
    }
}
