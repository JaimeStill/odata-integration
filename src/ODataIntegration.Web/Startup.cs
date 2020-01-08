using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ODataIntegration.Core.Extensions;
using ODataIntegration.Core.Logging;
using ODataIntegration.Core.Upload;
using ODataIntegration.Data;
using ODataIntegration.Data.Entities;

namespace ODataIntegration.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            Logger = new LogProvider
            {
                LogDirectory = Configuration.GetValue<string>("LogDirectory") ??
                    $@"{Environment.WebRootPath}\logs"
            };
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        public LogProvider Logger { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                if (Environment.IsDevelopment())
                {
                    options.UseSqlServer(Configuration.GetConnectionString("Dev"));
                    options.EnableSensitiveDataLogging();
                }
                else if (Environment.IsStaging())
                {
                    options.UseSqlServer(Configuration.GetConnectionString("Test"));
                    options.EnableSensitiveDataLogging();
                }
                else
                {
                    options.UseSqlServer(Configuration.GetConnectionString("Project"));
                }
            });

            services.AddOData();

            services
                .AddMvc(options => options.EnableEndpointRouting = false)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            if (Environment.IsDevelopment())
            {
                services.AddSingleton(new UploadConfig
                {
                    DirectoryBasePath = $@"{Environment.ContentRootPath}/wwwroot/files/",
                    UrlBasePath = "/files/"
                });

                services
                    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            else
            {
                services.AddSingleton(new UploadConfig
                {
                    DirectoryBasePath = Configuration.GetValue<string>("AppDirectoryBasePath"),
                    UrlBasePath = Configuration.GetValue<string>("AppUrlBasePath")
                });
            }

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Logger.LogDirectory),
                RequestPath = "/logs"
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Logger.LogDirectory),
                RequestPath = "/logs"
            });
            
            app.UseExceptionHandler(err => err.HandleError(Logger));

            app.UseMvc(builder =>
            {
                builder.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}"
                );

                builder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                builder.MapODataServiceRoute("odata", "odata", EdmModelBuilder.GetEdmModel());
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
