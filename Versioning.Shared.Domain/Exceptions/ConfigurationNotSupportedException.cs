using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.domain.Expceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace Versioning.Shared.Domain.Exceptions
{
    public class ConfigurationNotSupportedException : DomainException
    {
        private readonly MfeConfigurationName name;

        public ConfigurationNotSupportedException(MfeConfigurationName name) : base()
        {
            this.name = name;
        }
        public override string Message => $"The configuration {this.name.Value} is not supported.";

        public override string ErrorCode => "no_active_configuration_exists";
    }
}
