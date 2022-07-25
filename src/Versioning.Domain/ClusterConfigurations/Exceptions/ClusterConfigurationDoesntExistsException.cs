using Shared.Domain.Exceptions;
using Versioning.Domain.Shared.ValueObjects;

namespace Versioning.Domain.ClusterConfigurations.Exceptions
{
    public sealed class ClusterConfigurationDoesntExistsException : DomainException
    {
        private readonly ClusterId id;
        private readonly MfeId name;
        private readonly ConfigurationName? configurationName;

        public ClusterConfigurationDoesntExistsException(ClusterId id, MfeId name, ConfigurationName? configurationName) : base()
        {
            this.id = id;
            this.name = name;
            this.configurationName = configurationName;
        }
        public override string Message => $"The '{(this.configurationName != null ? this.configurationName.Value : "active")}' microfrontend configuration for cluster={this.id.Value} and mfeid={this.name.Value} doesn't exists";

        public override string ErrorCode => "cluster_config_doesnt_exists";

    }
}
