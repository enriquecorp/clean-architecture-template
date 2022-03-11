using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Versioning.Shared.Domain.ValueObjects
{
    public sealed class MfeVersion
    {
        private readonly string version;

        public MfeVersion(string version)
        {
            this.version = version;
        }
    }
}
