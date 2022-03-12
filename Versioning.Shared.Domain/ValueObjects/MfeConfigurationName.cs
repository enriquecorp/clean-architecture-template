using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.domain.ValueObjects;

namespace Versioning.Shared.Domain.ValueObjects
{
    public sealed class MfeConfigurationName : StringValueObject
    {
        public MfeConfigurationName(string value) : base(value.Trim().ToLower())
        {
        }
    }
}
