using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Versioning.Shared.Domain.ValueObjects
{
    public sealed class VersionList
    {
        private readonly Dictionary<string, MfeVersion> versions = new();
        public MfeVersion this[string index]
        {
            get => this.versions[index];
            set => this.versions[index] = value;
        }
    }
}
