using Bogus;

namespace Versioning.Shared.Tests.Domain.Simples
{
    public static class MotherCreator
    {
        public static Faker<T> Random<T>() where T : class
        {
            return new Faker<T>();
        }

        public static Randomizer Random()
        {
            return new Faker().Random;
        }
    }
}
