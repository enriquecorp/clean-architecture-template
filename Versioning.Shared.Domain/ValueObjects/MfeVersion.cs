using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.domain.ValueObjects;

namespace Versioning.Shared.Domain.ValueObjects
{
    public sealed class MfeVersion : StringValueObject
    {
        // private readonly string version;

        public MfeVersion(string version) : base(version)
        {
            //this.version = version;
        }
    }
}
