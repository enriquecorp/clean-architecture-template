namespace shared.domain.Bus.Command
{
    public class UnhandledCommandException : Exception
    {
        public UnhandledCommandException(ICommand command) : base(
            $"The command {command} has not a command handler associated")
        {
        }
    }

}
