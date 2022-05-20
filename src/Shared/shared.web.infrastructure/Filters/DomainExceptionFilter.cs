using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using shared.domain.Expceptions;
using shared.web.infrastructure.Attributes;

namespace shared.web.infrastructure.Filters
{
    public sealed class DomainExceptionFilter : IExceptionFilter // IActionFilter // 
    {
        //public void OnActionExecuted(ActionExecutedContext context)
        //{
        //    //if (context.Exception is null or not DomainException)
        //    //{
        //    //    return;
        //    //}

        //    //var mapperAttribute = context.ActionDescriptor.FilterDescriptors
        //    //.Select(x => x.Filter).OfType<DomainExceptionMapperAttribute>().FirstOrDefault(); //.Select(f=>f.ExceptionTypeName==context.Exception.GetType().ToString())

        //    //if (mapperAttribute != null)
        //    //{
        //    //    //var httpStatusCode = mapperAttribute.Mapping[context.Exception.GetType().ToString()];
        //    //    var httpStatusCode = mapperAttribute.HttpStatusCode;
        //    //    //HttpStatusCode.
        //    //    //context.Result = new ObjectResult(exception.Value)
        //    //    //{
        //    //    //    StatusCode = exception.Status,
        //    //    //};
        //    //    context.Result = new ContentResult
        //    //    {
        //    //        Content = context.Exception.ToString()
        //    //    };
        //    //    context.ExceptionHandled = true;
        //    //}
        //}

        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    //nothing here
        //}

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is null or not DomainException)
            {
                return;
            }
            var exception = context.Exception as DomainException;
            //var mapperAttribute = context.ActionDescriptor.FilterDescriptors
            //.Select(x => x.Filter).OfType<DomainExceptionMapperAttribute>().FirstOrDefault(); //.Select(f=>f.ExceptionTypeName==context.Exception.GetType().ToString())
            var attribute = context.ActionDescriptor.FilterDescriptors
            .Select(x => x.Filter).OfType<DomainExceptionMapperAttribute>().Where(a => a.ExceptionTypeName == exception?.GetType().Name).FirstOrDefault();

            //if (mapperAttribute != null)
            if (attribute != null)
            {
                var httpStatusCode = (int)attribute.HttpStatusCode;
                var errorCode = exception?.ErrorCode;
                //context.Result = new JsonResult(context.Exception.Message)
                //{
                //    StatusCode = httpStatusCode
                //};

                var error = new ApiErrorResponse() { ErrorKey = errorCode ?? "no_error_code_defined", ErrorDescription = context.Exception.Message, HttpStatusCode = httpStatusCode };
                context.Result = new JsonResult(error)
                {
                    StatusCode = httpStatusCode
                };
                // context.ExceptionHandled = true;
                return;
            }
            throw new KeyNotFoundException($"There is no http status code mapped for {context.Exception} domain exception");
        }
    }
}
