using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MfeConfigurations.Application.Create
{
    public sealed class MfeTenantConfigurationRequest
    {
        public string TenantId { get; set; }
        public string MfeId { get; set; }
        public Dictionary<string, string> Configurations { get; set; }
    }
}
