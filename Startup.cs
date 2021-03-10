using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvDotNetAPI.Models;
using Microsoft.EntityFrameworkCore;
using Serilog.Extensions.Logging.File;

namespace AdvDotNetAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options => 
                {
                    options.SuppressModelStateInvalidFilter = true;
                })
                .AddXmlDataContractSerializerFormatters();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdvDotNetAPI", Version = "v1" });
            });
            //services.AddDbContext<MedicalDataContext>(opt => opt.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=MedicalDB;Integrated Security=True"));
            services.AddDbContext<MedicalDataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdvDotNetAPI v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Logger configuration
            loggerFactory.AddFile("Logs/log-{Date}.txt", LogLevel.Error, null, false, 1073741824, 31,
                "{Message}{NewLine}{Exception}");
        }
    }
}
