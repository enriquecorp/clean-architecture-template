using System.ComponentModel.DataAnnotations;

namespace MfeTenantConfigurations.Application.UpdateActiveConfiguration
{
    public sealed class ActiveConfigurationRequest
    {
        [Required]
        public string MfeId { get; set; } = string.Empty;
        public string ActiveConfiguration { get; set; }
        public List<string> Tenants { get; set; } = new();
    }
}
