using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MfeGlobalConfigurations.Application.Update
{
    public sealed class MfeGlobalConfigurationRequest
    {
        public string MfeId { get; set; } = string.Empty;
        public string ActiveConfiguration { get; set; } = string.Empty;

        // ConfigurationVersions
        public Dictionary<string, string> Configurations { get; set; }
    }
}
