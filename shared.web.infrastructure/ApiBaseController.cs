
using Microsoft.AspNetCore.Mvc;
using shared.domain.bus.query;
using shared.domain.bus.command;

namespace shared.web.infrastructure
{
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
