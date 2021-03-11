using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


/// <summary>
/// Adapted from https://elanderson.net/2019/12/log-requests-and-responses-in-asp-net-core-3/
/// </summary>
namespace AdvDotNetAPI.Middleware
{
    public static class ResponseLoggingMiddlewareExtension
    {
            public static IApplicationBuilder UseResponseLoggingMiddleware(this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<ResponseLoggingMiddleware>();
            }
        
    }
}
