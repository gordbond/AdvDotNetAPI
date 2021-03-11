using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AdvDotNetAPI
{
    public class Program
    {
        /// <summary>
        /// Main method for the program
        /// </summary>
        /// <param name="args">Returns a string of args to the web host builder</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// IWebHostBuilder creates a default builder which configures the Kestrel builder and adds details to Kestrel Builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns>IWebHostBuilder</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
           .UseStartup<Startup>()
           .ConfigureKestrel((context, options) =>
           {
               options.Limits.MaxConcurrentConnections = 150;
               options.Limits.MaxConcurrentUpgradedConnections = 150;
               options.Limits.MaxRequestBodySize = int.MaxValue;
               options.Limits.MinRequestBodyDataRate = new MinDataRate(100, TimeSpan.FromSeconds(15));
               options.Limits.MinResponseDataRate = new MinDataRate(100, TimeSpan.FromSeconds(15));
               options.Listen(IPAddress.Loopback, 5000);
           });


    }
}
