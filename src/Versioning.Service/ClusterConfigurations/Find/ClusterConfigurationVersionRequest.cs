namespace Versioning.Service.ClusterConfigurations.Find
{
    public sealed class ClusterConfigurationVersionRequest
    {
        public string MfeId { get; set; }
        public string? Configuration { get; set; }
    }
}
