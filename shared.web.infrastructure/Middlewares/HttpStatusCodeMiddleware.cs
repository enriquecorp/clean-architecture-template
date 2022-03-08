using System;
using System.IO;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
//using VersioningService.Core.Exceptions;

namespace shared.web.infrastructure.Middlewares
{
    public class HttpStatusCodeMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<HttpStatusCodeMiddleware> logger;

        public HttpStatusCodeMiddleware(RequestDelegate next, ILogger<HttpStatusCodeMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                return;
            }

            try
            {
                context.Request.EnableBuffering();
                await next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (ex)
                {
                    //case ApiException e:
                    //    // Custom application error
                    //    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    //    await WriteAndLogResponseAsync(ex, context, HttpStatusCode.BadRequest, LogLevel.Error, "BadRequest Exception! " + ex.Message);
                    //    break;
                    //case NotFoundException e:
                    //    // Not found error
                    //    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    //    await WriteAndLogResponseAsync(ex, context, HttpStatusCode.NotFound, LogLevel.Error, "Not Found Exception! " + ex.Message);
                    //    break;
                    //case ValidationException e:
                    //    // Validatoon Error
                    //    context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    //    await WriteAndLogResponseAsync(ex, context, HttpStatusCode.UnprocessableEntity, LogLevel.Error, "Validation Exception! " + ex.Message);
                    //    break;
                    //case AuthenticationException e:

                    //    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    //    await WriteAndLogResponseAsync(ex, context, HttpStatusCode.Unauthorized, LogLevel.Error, "Authentication Exception! " + ex.Message);
                    //    break;
                    default:
                        // Unhandled error
                        // context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        await WriteAndLogResponseAsync(ex, context, HttpStatusCode.InternalServerError, LogLevel.Error, "Server Error! " + ex.Message);
                        break;
                }
            }
        }

        private async Task WriteAndLogResponseAsync(Exception ex, HttpContext httpContext, HttpStatusCode httpStatusCode, LogLevel logLevel, string? alternateMessage = null)
        {
            string requestBody = string.Empty;
            if (httpContext.Request.Body.CanSeek)
            {
                httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(httpContext.Request.Body))
                {
                    requestBody = JsonConvert.SerializeObject(await sr.ReadToEndAsync());
                }
            }

            StringValues authorization;
            httpContext.Request.Headers.TryGetValue("Authorization", out authorization);
            var customDetails = new StringBuilder();

            customDetails.AppendFormat("\n Service URL       :").Append(httpContext.Request?.Path.ToString())
                         .AppendFormat("\n Request Method    :").Append(httpContext.Request?.Method)
                         .AppendFormat("\n Request Body      :").Append(requestBody)
                         .AppendFormat("\n Authorization     :").Append(authorization)
                         .AppendFormat("\n Content Type      :").Append(httpContext.Request?.Headers["Content-type"].ToString())
                         .AppendFormat("\n Cookie            :").Append(httpContext.Request?.Headers["Cookie"].ToString())
                         .AppendFormat("\n Host              :").Append(httpContext.Request?.Headers["Host"].ToString())
                         .AppendFormat("\n Referer           :").Append(httpContext.Request?.Headers["Referer"].ToString())
                         .AppendFormat("\n Origin            :").Append(httpContext.Request?.Headers["Origin"].ToString())
                         .AppendFormat("\n User Agent        :").Append(httpContext.Request?.Headers["User-Agent"].ToString())
                         .AppendFormat("\n Error Message     :").Append(ex.Message);
            logger.Log(logLevel, ex, customDetails.ToString());

            if (httpContext.Response.HasStarted)
            {
                logger.LogError("The response has already started, the http status code middleware won't be executed");
            }

            string responseMessage = JsonConvert.SerializeObject(
                new
                {
                    Message = string.IsNullOrWhiteSpace(ex.Message) ? alternateMessage : ex.Message,
                    // StackTrace = string.IsNullOrWhiteSpace(alternateMessage)? ex.StackTrace: alternateMessage
                });
            httpContext.Response.Clear();
            httpContext.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
            httpContext.Response.StatusCode = (int)httpStatusCode;
            await httpContext.Response.WriteAsync(responseMessage, Encoding.UTF8);

        }
    }
}
