using Shared.Domain.Exceptions;
using Versioning.Domain.Shared.ValueObjects;

namespace Versioning.Domain.ClusterConfigurations.Exceptions
{
    public sealed class ClusterInvalidActiveConfigurationException : DomainException
    {
        private readonly ClusterId id;
        private readonly MfeId name;
        private readonly ConfigurationName? configurationName;

        public ClusterInvalidActiveConfigurationException(ClusterId id, MfeId name, ConfigurationName? configurationName) : base()
        {
            this.id = id;
            this.name = name;
            this.configurationName = configurationName;
        }
        public override string Message => $"The '{(this.configurationName != null ? this.configurationName.Value : "active")}' configuration requested for cluster={this.id.Value} and mfeid={this.name.Value} is invalid";

        public override string ErrorCode => "cluster_invalid_active_configuration";

    }
}
