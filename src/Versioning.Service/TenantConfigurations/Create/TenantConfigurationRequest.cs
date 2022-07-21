namespace Versioning.Service.TenantConfigurations.Create
{
    public sealed class TenantConfigurationRequest
    {
        public string TenantId { get; set; }
        public string MfeId { get; set; }
        public Dictionary<string, string> Configurations { get; set; }
        public string? ActiveConfiguration { get; set; }
    }
}
