﻿using Versioning.Shared.Domain.Constants;
using Versioning.Shared.Domain.ValueObjects;
using Versioning.Shared.Tests.Domain.Simples;

namespace Versioning.Shared.Tests.Domain
{
    public sealed class ConfigurationListMother
    {


        public static ConfigurationList Create(Dictionary<string, string> configurations)
        {
            return new ConfigurationList(configurations);
        }

        public static ConfigurationList First(int size)
        {
            size = size > 3 ? 3 : size;
            var configurations = new Dictionary<string, string>();
            for (int i = 0; i < IntegerMother.Between(1, size); i++)
            {
                configurations.Add(Configuration.SupportedConfigurations[i], UrlMother.Random());
            }
            return Create(configurations);
        }

        public static ConfigurationList Random()
        {
            return First(IntegerMother.Between(1, 3));
        }
    }
}
