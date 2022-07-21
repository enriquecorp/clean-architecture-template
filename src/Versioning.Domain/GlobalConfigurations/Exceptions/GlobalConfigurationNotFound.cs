using Shared.Domain.Exceptions;
using Versioning.Domain.ValueObjects;

namespace Versioning.Domain.GlobalConfigurations.Exceptions
{
    public sealed class GlobalConfigurationNotFound : DomainException
    {
        private readonly MfeId name;

        public GlobalConfigurationNotFound(MfeId name) : base()
        {
            this.name = name;
        }
        public override string Message => $"The microfrontend global configuration for the microfontend mfeid={this.name.Value} doesn't exists";

        public override string ErrorCode => "global_config_no_exists";

    }
}
