﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MfeConfigurations.Application.Update
{

    public sealed class MfeTenantConfigurationUpdateRequest
    {
        public List<string> Tenants { get; set; }
        public string MfeId { get; set; }
        public string Configuration { get; set; }
        public string Version { get; set; }
        public bool SetConfigurationAsActive { get; set; } = false;
    }
}
