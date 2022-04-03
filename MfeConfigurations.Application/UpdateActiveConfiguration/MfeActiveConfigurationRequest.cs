using System.ComponentModel.DataAnnotations;

namespace MfeConfigurations.Application.UpdateActiveConfiguration
{
    public sealed class MfeActiveConfigurationRequest
    {
        [Required]
        public string MfeId { get; set; } = string.Empty;
        public string ActiveConfiguration { get; set; }
        public List<string> Tenants { get; set; } = new();
    }
}
