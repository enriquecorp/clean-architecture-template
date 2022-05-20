namespace MfeTenantConfigurations.Application.Find
{
    public sealed class ConfigurationVersionRequest
    {
        public string MfeId { get; set; }
        public string? Configuration { get; set; }
    }
}
