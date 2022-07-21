namespace Versioning.Service.ClusterConfigurations.Update
{

    public sealed class ClusterConfigurationUpdateRequest
    {
        public List<string> Clusters { get; set; }
        public string MfeId { get; set; }
        public string Configuration { get; set; }
        public string VersionUrl { get; set; }
        public bool SetConfigurationAsActive { get; set; } = false;
    }
}
