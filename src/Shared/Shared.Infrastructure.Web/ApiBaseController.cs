
using Microsoft.AspNetCore.Mvc;

namespace Shared.Infrastructure.Web
{
    [Produces("application/json")]
    public abstract class ApiBaseController : Controller
    {
        public ApiBaseController()
        {

        }
        // abstract protected IList<string> Exceptions();

        //protected Response ask(IQuery query)
        //{
        //    return $this->queryBus->ask($query);
        //}

        //protected void dispatch(ICommand $command): void
        //{
        //    $this->commandBus->dispatch($command);
        //}
    }
}
