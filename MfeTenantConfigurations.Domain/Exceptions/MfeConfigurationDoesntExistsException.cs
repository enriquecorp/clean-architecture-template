using shared.domain.Expceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeTenantConfigurations.Domain.Exceptions
{
    public sealed class MfeConfigurationDoesntExistsException : DomainException
    {
        private readonly TenantId id;
        private readonly MfeId name;
        private readonly MfeConfigurationName? configurationName;

        public MfeConfigurationDoesntExistsException(TenantId id, MfeId name, MfeConfigurationName? configurationName) : base()
        {
            this.id = id;
            this.name = name;
            this.configurationName = configurationName;
        }
        public override string Message => $"The microfrontend '{(this.configurationName != null ? this.configurationName.Value : "`active`")}' configuration for tenantid={this.id.Value} and mfeid={this.name.Value} doesn't exists";

        public override string ErrorCode => "tenant_config_doesnt_exists";

    }
}
