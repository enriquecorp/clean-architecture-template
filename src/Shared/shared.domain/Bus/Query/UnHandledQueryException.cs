namespace Shared.Domain.Bus.Query
{
    public class UnHandledQueryException : Exception
    {
        public UnHandledQueryException(IQuery query) : base(
            $"The query {query} has not a query handler associated")
        {
        }
    }
}
