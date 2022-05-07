﻿namespace MfeConfigurations.Application.Create
{
    public sealed class MfeTenantConfigurationRequest
    {
        public string TenantId { get; set; }
        public string MfeId { get; set; }
        public Dictionary<string, string> Configurations { get; set; }
        public string? ActiveConfiguration { get; set; }
    }
}
