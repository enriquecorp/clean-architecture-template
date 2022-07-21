namespace Versioning.Service.TenantConfigurations.Update
{

    public sealed class TenantConfigurationUpdateRequest
    {
        public List<string> Tenants { get; set; }
        public string MfeId { get; set; }
        public string Configuration { get; set; }
        public string VersionUrl { get; set; }
        public bool SetConfigurationAsActive { get; set; } = false;
    }
}
