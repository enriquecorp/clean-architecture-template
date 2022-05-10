using shared.domain.Expceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeTenantConfigurations.Domain.Exceptions
{
    public sealed class NoActiveConfigurationExistsException : DomainException
    {
        private readonly TenantId id;
        private readonly MfeId name;

        public NoActiveConfigurationExistsException(TenantId id, MfeId name) : base()
        {
            this.id = id;
            this.name = name;
        }
        public override string Message => $"There is no an active configuration for tenantid={this.id.Value} and mfeid={this.name.Value}";

        public override string ErrorCode => "no_active_configuration_exists";

    }
}
