namespace Versioning.Shared.Tests.Domain.Simples
{
    public static class UuidMother
    {
        public static string Random()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
