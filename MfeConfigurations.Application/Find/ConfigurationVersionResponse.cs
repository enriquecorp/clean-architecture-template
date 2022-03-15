using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MfeConfigurations.Application.Find
{
    public sealed class ConfigurationVersionResponse
    {
        public string Version { get; set; }
        public string MfeUrl { get; set; } = string.Empty;
    }
}
