namespace shared.domain.Bus.Query
{
    internal interface IQueryHandler<TQuery, TResponse> where TQuery : IQuery
    {
        Task<TResponse> Handle(TQuery query);
    }
}
