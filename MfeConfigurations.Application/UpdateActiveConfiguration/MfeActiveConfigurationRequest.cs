using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
