using shared.domain.Expceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeClusterConfigurations.Domain.Exceptions
{
    public sealed class MfeClusterConfigurationDoesntExistsException : DomainException
    {
        private readonly ClusterId id;
        private readonly MfeId name;
        private readonly MfeConfigurationName? configurationName;

        public MfeClusterConfigurationDoesntExistsException(ClusterId id, MfeId name, MfeConfigurationName? configurationName) : base()
        {
            this.id = id;
            this.name = name;
            this.configurationName = configurationName;
        }
        public override string Message => $"The '{(this.configurationName != null ? this.configurationName.Value : "active")}' microfrontend configuration for cluster={this.id.Value} and mfeid={this.name.Value} doesn't exists";

        public override string ErrorCode => "cluster_config_doesnt_exists";

    }
}
