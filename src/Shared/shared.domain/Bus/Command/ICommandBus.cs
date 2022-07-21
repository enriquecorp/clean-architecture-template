namespace Shared.Domain.Bus.Command
{
    public interface ICommandBus
    {
        Task Dispatch(ICommand command);
    }
}
