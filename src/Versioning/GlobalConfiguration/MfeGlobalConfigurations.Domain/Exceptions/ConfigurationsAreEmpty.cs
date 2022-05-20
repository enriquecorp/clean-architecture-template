using shared.domain.Expceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Domain.Exceptions
{
    public sealed class ConfigurationsAreEmpty : DomainException
    {
        private readonly MfeId name;

        public ConfigurationsAreEmpty(MfeId name) : base()
        {
            this.name = name;
        }
        public override string Message => $"The configurations assigned to mfeid={this.name.Value} must not be empty";

        public override string ErrorCode => "versions_are_empty";

    }
}
