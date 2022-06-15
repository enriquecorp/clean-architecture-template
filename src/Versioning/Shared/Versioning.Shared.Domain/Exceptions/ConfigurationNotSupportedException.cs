using shared.domain.Expceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace Versioning.Shared.Domain.Exceptions
{
    public class ConfigurationNotSupportedException : DomainException
    {
        private readonly ConfigurationName name;

        public ConfigurationNotSupportedException(ConfigurationName name) : base()
        {
            this.name = name;
        }
        public override string Message => $"The configuration {this.name.Value} is not supported.";

        public override string ErrorCode => "configuration_not_supported";
    }
}
