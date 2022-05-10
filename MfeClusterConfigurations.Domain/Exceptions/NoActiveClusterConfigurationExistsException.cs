using shared.domain.Expceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeClusterConfigurations.Domain.Exceptions
{
    public sealed class NoActiveClusterConfigurationExistsException : DomainException
    {
        private readonly TenantId id;
        private readonly MfeId name;

        public NoActiveClusterConfigurationExistsException(TenantId id, MfeId name) : base()
        {
            this.id = id;
            this.name = name;
        }
        public override string Message => $"There is no an active configuration for clusterid={this.id.Value} and mfeid={this.name.Value}";

        public override string ErrorCode => "no_active_configuration_exists";

    }
}
