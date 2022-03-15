using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.domain.Expceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Domain.Exceptions
{
    public sealed class MfeConfigurationDoesntExistsException : DomainException
    {
        private readonly TenantId id;
        private readonly MfeId name;
        private readonly MfeConfigurationName? config;

        public MfeConfigurationDoesntExistsException(TenantId id, MfeId name, MfeConfigurationName? config): base()
        {
            this.id = id;
            this.name = name;
            this.config = config;
        }
        public override string Message => $"The microfrontend '{(this.config!=null?this.config.Value:"`active`")}' configuration for tenantid={this.id.Value} and mfeid={this.name.Value} doesn't exists";

        public override string ErrorCode => "tenant_config_doesnt_exists";
        
    }
}
