using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using shared.domain.Expceptions;

namespace shared.web.infrastructure.Attributes
{
    public sealed class DomainExceptionMapperAttribute : Attribute, IFilterMetadata
    {
        public string ExceptionTypeName { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
