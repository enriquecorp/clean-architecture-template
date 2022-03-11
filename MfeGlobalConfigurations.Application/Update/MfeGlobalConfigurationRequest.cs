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
        public Dictionary<string, string> Versions { get; set; }
    }
}
