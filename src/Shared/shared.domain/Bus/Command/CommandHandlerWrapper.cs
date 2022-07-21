namespace Shared.Domain.Bus.Command
{
    internal abstract class CommandHandlerWrapper
    {
        public abstract Task Handle(ICommand command, IServiceProvider provider);
    }

    internal class CommandHandlerWrapper<TCommand> : CommandHandlerWrapper
        where TCommand : ICommand
    {
        public override async Task Handle(ICommand domainEvent, IServiceProvider provider)
        {
            if (provider.GetService(typeof(ICommandHandler<TCommand>)) is not ICommandHandler<TCommand> handler)
            {
                throw new InvalidOperationException();
            }
            await handler.Handle((TCommand)domainEvent);
        }
    }
}
