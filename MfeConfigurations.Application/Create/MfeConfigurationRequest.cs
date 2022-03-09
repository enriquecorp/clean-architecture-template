using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MfeConfigurations.Application.Create
{
    public sealed class MfeConfigurationRequest
    {
        public string TenantId { get; set; } = string.Empty;
        public string MfeId { get; set; } = string.Empty;
        public Dictionary<string, string> Configurations { get; set; }
    }
}
