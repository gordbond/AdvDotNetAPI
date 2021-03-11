/* We, Gord Bond, student number 000786196, Tiago Franco de Goes Teles, student number 000786450, 
 * Olaoluwa Anthony-Egorp, student number 000776467, and Mitchell Aninyang, student number 000796709, 
 * certify that all code submitted is our own work; that we have not copied it from any other source. 
 * We also certify that we have not allowed our work to be copied by others.
 */
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
        //MiddleWare variables being declares
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        /// <summary>
        /// Method that logs middleware by Creating the Logger
        /// </summary>
        /// <param name="next">RequestDelegate Object</param>
        /// <param name="loggerFactory">ILoggerFactory Object</param>
        public ResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory
                      .CreateLogger<ResponseLoggingMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }
        /// <summary>
        /// Async method invoke that runs asynchronously that logs the response of the HttpContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            await LogResponse(context);
        }


        /// <summary>
        /// Log Response method that returns back a response based on the HttpStatus Code from a Switch Case
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>Custom Message based on HttpStatus</returns>
        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;
            await _next(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string customMessage = "";
            
            //Custom Messages that come with Status Code
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
