using shared.domain.Expceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeClusterConfigurations.Domain.Exceptions
{
    public sealed class MfeClusterConfigurationDoesntExistsException : DomainException
    {
        private readonly TenantId id;
        private readonly MfeId name;
        private readonly MfeConfigurationName? configurationName;

        public MfeClusterConfigurationDoesntExistsException(TenantId id, MfeId name, MfeConfigurationName? configurationName) : base()
        {
            this.id = id;
            this.name = name;
            this.configurationName = configurationName;
        }
        public override string Message => $"The microfrontend '{(this.configurationName != null ? this.configurationName.Value : "`active`")}' configuration for cluster={this.id.Value} and mfeid={this.name.Value} doesn't exists";

        public override string ErrorCode => "tenant_config_doesnt_exists";

    }
}
