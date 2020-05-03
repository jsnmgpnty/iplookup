using System.Net;
using IpLookup.Common;
using IpLookup.Common.Web.Extensions;
using IpLookup.Common.Web.Infrastructure;
using IpLookup.Common.Web.Infrastructure.Logging;
using IpProcessorApi.Http.Services;
using IpProcessorApi.Http.Settings;
using IpProcessorApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;

namespace IpProcessorApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        const string ServiceName = "IP Processor API";

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOptions();

            // Register util services
            services.AddSingleton(Log.Logger);
            services.AddSingleton<IConfigurationHelper, ConfigurationHelperJson>();
            services.AddSingleton<ILogHelper, LogHelper>();

            // Register json settings
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,

            };
            serializerSettings.Converters.Add(new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() });
            services.AddSingleton(serializerSettings);

            // Register services
            services.AddSingleton<IViewDnsService, ViewDnsService>();

            // Register http services
            services.AddSingleton<IHttpViewDnsService, HttpViewDnsService>();
            services.AddSingleton(typeof(HttpViewDnsServiceSettings), Configuration.GetSection("Services:ViewDns").Get<HttpViewDnsServiceSettings>());

            // Add swagger doc
            services.AddSwaggerDocumentation(ServiceName);

            // cors setup
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                    );
            });

            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
                options.EnableEndpointRouting = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<HeaderLogger>();
            app.UseMiddleware<RequestLogger>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // cors config
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            // swagger configuration
            app.UseSwaggerDocumentation(ServiceName);

            // global exception handler
            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    if (ex != null)
                    {
                        await context.Response.WriteAsync(ex.Error.Message).ConfigureAwait(false);
                    }
                });
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
