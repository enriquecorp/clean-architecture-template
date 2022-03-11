using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using shared.domain.Expceptions;
using shared.web.infrastructure.Attributes;

namespace shared.web.infrastructure.Filters
{
    public sealed class DomainExceptionFilter : IExceptionFilter // IActionFilter // 
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //if (context.Exception is null or not DomainException)
            //{
            //    return;
            //}

            //var mapperAttribute = context.ActionDescriptor.FilterDescriptors
            //.Select(x => x.Filter).OfType<DomainExceptionMapperAttribute>().FirstOrDefault(); //.Select(f=>f.ExceptionTypeName==context.Exception.GetType().ToString())

            //if (mapperAttribute != null)
            //{
            //    //var httpStatusCode = mapperAttribute.Mapping[context.Exception.GetType().ToString()];
            //    var httpStatusCode = mapperAttribute.HttpStatusCode;
            //    //HttpStatusCode.
            //    //context.Result = new ObjectResult(exception.Value)
            //    //{
            //    //    StatusCode = exception.Status,
            //    //};
            //    context.Result = new ContentResult
            //    {
            //        Content = context.Exception.ToString()
            //    };
            //    context.ExceptionHandled = true;
            //}
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //nothing here
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is null or not DomainException)
            {
                return;
            }

            var mapperAttribute = context.ActionDescriptor.FilterDescriptors
            .Select(x => x.Filter).OfType<DomainExceptionMapperAttribute>().FirstOrDefault(); //.Select(f=>f.ExceptionTypeName==context.Exception.GetType().ToString())

            if (mapperAttribute != null)
            {
                //var httpStatusCode = mapperAttribute.Mapping[context.Exception.GetType().ToString()];
                var httpStatusCode = mapperAttribute.HttpStatusCode;
                //HttpStatusCode.
                //context.Result = new ObjectResult(exception.Value)
                //{
                //    StatusCode = exception.Status,
                //};
                context.Result = new ContentResult
                {
                    Content = context.Exception.Message
                };
                // context.ExceptionHandled = true;
            }
        }
    }
}
