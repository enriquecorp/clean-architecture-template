namespace Versioning.Shared.Domain.Constants
{
    /// <summary>
    /// This class is just temporarly because we should have all supported configurations stored by tenant
    /// </summary>
    public class Configuration
    {
        public static readonly string[] SupportedConfigurations = { "current", "previous", "preview" };
    }
}
