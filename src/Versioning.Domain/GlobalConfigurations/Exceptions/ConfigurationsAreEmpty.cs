using Shared.Domain.Exceptions;
using Versioning.Domain.Shared.ValueObjects;

namespace Versioning.Domain.GlobalConfigurations.Exceptions
{
    public sealed class ConfigurationsAreEmpty : DomainException
    {
        private readonly MfeId name;

        public ConfigurationsAreEmpty(MfeId name) : base()
        {
            this.name = name;
        }
        public override string Message => $"The configurations assigned to mfeid={this.name.Value} must not be empty";

        public override string ErrorCode => "configurations_are_empty";

    }
}
