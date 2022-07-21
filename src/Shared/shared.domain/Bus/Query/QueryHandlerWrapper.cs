namespace Shared.Domain.Bus.Query
{
    internal abstract class QueryHandlerWrapper<TResponse>
    {
        public abstract Task<TResponse> Handle(IQuery query, IServiceProvider provider);
    }

    internal class QueryHandlerWrapper<TQuery, TResponse> : QueryHandlerWrapper<TResponse>
        where TQuery : IQuery
    {
        public override async Task<TResponse> Handle(IQuery query, IServiceProvider provider)
        {
            if (provider.GetService(typeof(IQueryHandler<TQuery, TResponse>)) is not IQueryHandler<TQuery, TResponse> handler)
            {
                throw new InvalidOperationException("Unknow Query Service location");
            }

            return await handler.Handle((TQuery)query);
        }
    }
}
