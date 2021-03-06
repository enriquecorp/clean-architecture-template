using Shared.Domain.Exceptions;
using Versioning.Domain.Shared.ValueObjects;

namespace Versioning.Domain.TenantConfigurations.Exceptions
{
    public sealed class TenantConfigurationAlreadyExistsException : DomainException
    {
        private readonly TenantId id;
        private readonly MfeId name;

        public TenantConfigurationAlreadyExistsException(TenantId id, MfeId name) : base()
        {
            this.id = id;
            this.name = name;
        }
        public override string Message => $"The microfrontend configuration with tenantid={this.id.Value} and mfeid={this.name.Value} already exists";

        public override string ErrorCode => "mfeconfig_already_exists";

    }
}
