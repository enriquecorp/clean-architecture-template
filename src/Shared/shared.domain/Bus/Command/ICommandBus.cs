namespace shared.domain.Bus.Command
{
    public interface ICommandBus
    {
        Task Dispatch(ICommand command);
    }
}
