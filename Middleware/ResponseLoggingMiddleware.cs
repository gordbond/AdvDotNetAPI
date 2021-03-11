using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AdvDotNetAPI.Middleware
{
    /// <summary>
    /// Adapted from https://elanderson.net/2019/12/log-requests-and-responses-in-asp-net-core-3/
    /// </summary>
    public class ResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public ResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory
                      .CreateLogger<ResponseLoggingMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }
        public async Task Invoke(HttpContext context)
        {
            await LogResponse(context);
        }


        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;
            await _next(context);
            string customMessage = "";
            //var context2 = context.Features.Get<IExceptionHandlerFeature>();
            

            switch (context.Response.StatusCode) {
                case 200:
                    customMessage = "The GET or PUT completed successfully.";
                    break;
                case 201:
                    customMessage = "The POST operation completed successfully. ";
                    break;
                case 204:
                    customMessage = "The DELETE operation completed successfully.";
                    break;
                case 400:
                    customMessage = "A mandatory field is missing.";
                    break;
                case 404:
                    customMessage = "The resource was not found on the API.";
                    break;
                case 405:
                    customMessage = "An attempt to use the incorrect HTTP verb on an endpoint.";
                    break;
                case 406:
                    customMessage = "The HTTP Accept header is invalid.";
                    break;
                case 415:
                    customMessage = "The content is not in either XML or JSON format.";
                    break;
                case 500:
                    customMessage = "An unexpected error occured.";
                    break;
                default:
                    customMessage = "An error outside the scope of this lab occured";
                    break;

            }
            
            _logger.LogError($"Message: {customMessage}, Status code: {context.Response.StatusCode}, Id: {Guid.NewGuid()}");

            await responseBody.CopyToAsync(originalBodyStream);
            
        }
    }
}
