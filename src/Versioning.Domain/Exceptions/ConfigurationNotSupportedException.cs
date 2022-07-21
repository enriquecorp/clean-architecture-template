using Shared.Domain.Exceptions;
using Versioning.Domain.ValueObjects;

namespace Versioning.Domain.Exceptions
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
