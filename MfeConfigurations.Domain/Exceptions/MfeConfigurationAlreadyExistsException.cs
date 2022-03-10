using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.domain.Expceptions;

namespace MfeConfigurations.Domain.Exceptions
{
    public sealed class MfeConfigurationAlreadyExistsException : DomainException
    {
        private readonly MfeConfiguration configuration;

        public MfeConfigurationAlreadyExistsException(MfeConfiguration configuration): base()
        {
            this.configuration = configuration;
        }
        public override string Message => $"The microfrontend configuguration with tenantid={this.configuration.TenantId} and mfeid={this.configuration.MfeId} already exists";

        public override string ErrorCode => "mfeconfig_already_exists";
        
    }
}
