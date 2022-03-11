using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.domain.Expceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Domain.Exceptions
{
    public sealed class MfeGlobalConfigurationNotFound : DomainException
    {
        private readonly MfeId name;

        public MfeGlobalConfigurationNotFound(MfeId name) : base()
        {
            this.name = name;
        }
        public override string Message => $"The microfrontend global configuration for the microfontend mfeid={this.name.Value} doesn't exists";

        public override string ErrorCode => "global_config_no_exists";

    }
}
