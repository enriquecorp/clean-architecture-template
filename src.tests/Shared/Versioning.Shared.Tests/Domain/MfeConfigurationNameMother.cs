using Versioning.Shared.Domain.Constants;
using Versioning.Shared.Domain.ValueObjects;
using Versioning.Shared.Tests.Domain.Simples;

namespace Versioning.Shared.Tests.Domain
{
    public sealed class ConfigurationNameMother
    {
        public static ConfigurationName Create(string value)
        {
            return new ConfigurationName(value);
        }

        public static ConfigurationName Random()
        {
            var selected = IntegerMother.Between(1, 3);
            return Create(Configuration.SupportedConfigurations[selected - 1]);
        }
    }
}
