using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Shared.Infrastructure.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class DomainExceptionMapperAttribute : Attribute, IFilterMetadata
    {
        public string ExceptionTypeName { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
