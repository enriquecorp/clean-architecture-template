using Versioning.Shared.Domain.Constants;
using Versioning.Shared.Domain.ValueObjects;
using Versioning.Shared.Tests.Domain.Simples;

namespace Versioning.Shared.Tests.Domain
{
    public sealed class MfeConfigurationNameMother
    {
        public static MfeConfigurationName Create(string value)
        {
            return new MfeConfigurationName(value);
        }

        public static MfeConfigurationName Random()
        {
            var selected = IntegerMother.Between(1, 3);
            return Create(Configuration.SupportedConfigurations[selected - 1]);
        }
    }
}
