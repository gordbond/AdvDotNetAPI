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
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

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
