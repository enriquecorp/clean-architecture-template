using Versioning.Domain.Constants;
using Versioning.Domain.ValueObjects;
using Versioning.Shared.Tests.Domain.Simples;

namespace Versioning.Shared.Tests.Domain.ValueObjects
{
    public sealed class ConfigurationNameMother
    {
        public static ConfigurationName Create(string value)
        {
            return new ConfigurationName(value);
        }

        public static ConfigurationName Random()
        {
            var selected = IntegerMother.Between(1, Configuration.SupportedConfigurations.Length);
            return Create(Configuration.SupportedConfigurations[selected - 1]);
        }
    }
}
