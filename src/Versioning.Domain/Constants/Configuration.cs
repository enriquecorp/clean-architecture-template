﻿using System.Collections.Immutable;

namespace Versioning.Domain.Constants
{
    /// <summary>
    /// This class is just temporarly because we should have all supported configurations stored by tenant
    /// </summary>
    public class Configuration
    {
        public static readonly ImmutableArray<string> SupportedConfigurations =
            ImmutableArray.Create<string>(new string[] { "current", "previous", "preview" });
    }
}
