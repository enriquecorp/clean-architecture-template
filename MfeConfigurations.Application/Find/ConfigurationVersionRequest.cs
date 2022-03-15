using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MfeConfigurations.Application.Find
{
    public sealed class ConfigurationVersionRequest
    {
        public string MfeId { get; set; }
        public string? Configuration { get; set; }
    }
}
