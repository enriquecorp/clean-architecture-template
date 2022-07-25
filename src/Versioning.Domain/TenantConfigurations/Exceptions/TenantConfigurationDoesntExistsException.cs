using Shared.Domain.Exceptions;
using Versioning.Domain.Shared.ValueObjects;

namespace Versioning.Domain.TenantConfigurations.Exceptions
{
    public sealed class TenantConfigurationDoesntExistsException : DomainException
    {
        private readonly TenantId id;
        private readonly MfeId name;
        private readonly ConfigurationName? configurationName;

        public TenantConfigurationDoesntExistsException(TenantId id, MfeId name, ConfigurationName? configurationName) : base()
        {
            this.id = id;
            this.name = name;
            this.configurationName = configurationName;
        }
        public override string Message => $"The '{(this.configurationName != null ? this.configurationName.Value : "active")}' microfrontend configuration for tenantid={this.id.Value} and mfeid={this.name.Value} doesn't exists";

        public override string ErrorCode => "tenant_config_doesnt_exists";

    }
}
