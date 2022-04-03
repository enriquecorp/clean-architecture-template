using shared.domain.Expceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Domain.Exceptions
{
    public sealed class MfeVersionsAreEmpty : DomainException
    {
        private readonly MfeId name;

        public MfeVersionsAreEmpty(MfeId name) : base()
        {
            this.name = name;
        }
        public override string Message => $"The versions assigned to mfeid={this.name.Value} must not be empty";

        public override string ErrorCode => "versions_are_empty";

    }
}
