/* We, Gord Bond, student number 000786196, Tiago Franco de Goes Teles, student number 000786450, 
 * Olaoluwa Anthony-Egorp, student number 000776467, and Mitchell Aninyang, student number 000796709, 
 * certify that all code submitted is our own work; that we have not copied it from any other source. 
 * We also certify that we have not allowed our work to be copied by others.
 */
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
