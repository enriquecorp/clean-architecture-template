using shared.domain.Expceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeClusterConfigurations.Domain.Exceptions
{
    public sealed class MfeClusterInvalidActiveConfigurationException : DomainException
    {
        private readonly ClusterId id;
        private readonly MfeId name;
        private readonly MfeConfigurationName? configurationName;

        public MfeClusterInvalidActiveConfigurationException(ClusterId id, MfeId name, MfeConfigurationName? configurationName) : base()
        {
            this.id = id;
            this.name = name;
            this.configurationName = configurationName;
        }
        public override string Message => $"The '{(this.configurationName != null ? this.configurationName.Value : "active")}' configuration requested for cluster={this.id.Value} and mfeid={this.name.Value} is invalid";

        public override string ErrorCode => "cluster_invalid_active_configuration";

    }
}
