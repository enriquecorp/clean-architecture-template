namespace shared.domain.Bus.Query
{
    public interface IQueryBus
    {
        Task<TResponse> Ask<TResponse>(IQuery request);
    }
}
