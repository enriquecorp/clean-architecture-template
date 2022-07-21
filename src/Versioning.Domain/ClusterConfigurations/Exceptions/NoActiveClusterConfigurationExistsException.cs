using Shared.Domain.Exceptions;
using Versioning.Domain.ValueObjects;

namespace Versioning.Domain.ClusterConfigurations.Exceptions
{
    public sealed class NoActiveClusterConfigurationExistsException : DomainException
    {
        private readonly ClusterId id;
        private readonly MfeId name;

        public NoActiveClusterConfigurationExistsException(ClusterId id, MfeId name) : base()
        {
            this.id = id;
            this.name = name;
        }
        public override string Message => $"There is no an active configuration for clusterid={this.id.Value} and mfeid={this.name.Value}";

        public override string ErrorCode => "no_active_cluster_configuration_exists";

    }
}
