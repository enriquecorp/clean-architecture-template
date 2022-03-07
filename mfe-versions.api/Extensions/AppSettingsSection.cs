namespace mfe_versions.api.Extensions
{
    public class AppSettingsSection
    {
        public AppSettingsSection()
        {

        }
        public string KeyVaultName { get; set; } = default!;
        public bool ByPassKeyVault { get; set; } = default!;
    }
}
