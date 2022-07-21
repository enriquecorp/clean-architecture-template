namespace Shared.Domain.Bus.Query
{
    public interface IQueryBus
    {
        Task<TResponse> Ask<TResponse>(IQuery request);
    }
}
