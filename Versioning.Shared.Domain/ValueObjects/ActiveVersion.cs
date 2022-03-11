using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.domain.ValueObjects;

namespace Versioning.Shared.Domain.ValueObjects
{
    public class ActiveVersion : StringValueObject
    {
        public ActiveVersion(string value) : base(value)
        {
        }
    }
}
