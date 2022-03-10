using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.domain.Expceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Domain.Exceptions
{
    public sealed class MfeConfigurationAlreadyExistsException : DomainException
    {
        private readonly TenantId id;
        private readonly MfeId name;

        public MfeConfigurationAlreadyExistsException(TenantId id, MfeId name): base()
        {
            this.id = id;
            this.name = name;
        }
        public override string Message => $"The microfrontend configuration with tenantid={this.id.Value} and mfeid={this.name.Value} already exists";

        public override string ErrorCode => "mfeconfig_already_exists";
        
    }
}
