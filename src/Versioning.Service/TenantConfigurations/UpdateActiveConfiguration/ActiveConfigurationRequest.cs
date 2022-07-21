using System.ComponentModel.DataAnnotations;

namespace Versioning.Service.TenantConfigurations.UpdateActiveConfiguration
{
    public sealed class ActiveConfigurationRequest
    {
        [Required]
        public string MfeId { get; set; } = string.Empty;
        public string ActiveConfiguration { get; set; }
        public List<string> Tenants { get; set; } = new();
    }
}
