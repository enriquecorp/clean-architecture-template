using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.domain.ValueObjects
{
    public abstract class StringValueObject
    {
        public string Value { get; }

        public StringValueObject(string value)
        {
            this.Value = value;
        }

    }
}
