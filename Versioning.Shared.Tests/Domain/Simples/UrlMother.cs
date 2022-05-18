using Bogus;

namespace Versioning.Shared.Tests.Domain.Simples
{
    public class UrlMother
    {
        public static string Random()
        {
            return new Faker().Internet.Url();
        }

        public static string RandomDomainName()
        {
            return new Faker().Internet.DomainName();
        }
    }
}
