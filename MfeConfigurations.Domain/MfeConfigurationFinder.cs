using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Domain
{
    public sealed class MfeConfigurationFinder
    {
        private readonly IMfeConfigurationRepository repository;

        public MfeConfigurationFinder(IMfeConfigurationRepository repository)
        {
            this.repository = repository;
        }

        //public MfeConfiguration Execute(TenantId id, MfeId name)
        //{
        //    var configuration = this.repository.Search(id, name);
        //    if (configuration == null)
        //        throw new MfeConfigurationNotFound(id, name);
        //    return configuration;
        //}
    }
}
