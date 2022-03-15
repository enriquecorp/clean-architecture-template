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

        public MfeVersion(string version) : base(version.Trim().ToLower())
        {
            //this.version = version;
        }

        public static MfeVersion CreateEmpty()
        {
            return new MfeVersion("");
        }
    }
}
