namespace Versioning.Shared.Tests.Domain.Simples
{
    public class WordMother
    {
        public static string Random()
        {
            return MotherCreator.Random().Word();
        }
    }
}
