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
            return Create(WordMother.Random());
        }
    }
}
