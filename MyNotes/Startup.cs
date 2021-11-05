using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using MyNotes.DataAccess;
using MyNotes.DataAccess.InMemoryDb;
using MyNotes.HealthCheck;
using MyNotes.Services;
using RisGmp.Adapter.HealthCheck;
using Swashbuckle.AspNetCore.Filters;
using MyNotes.IdentityDb;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;

namespace MyNotes
{
    //https://stackoverflow.com/questions/58135672/adding-identity-server-authentication-to-net-core-3-app-fails-with-key-type-no
    public class Startup
    {
        internal static string BasePath;
        internal static string BuildVersion;
        internal static string BuildDate;
        internal static string AspNetCoreEnvironment;
        internal static string HostName;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            BasePath = string.IsNullOrWhiteSpace(Configuration["GlobalPrefix"]) ? "" : $"/{Configuration["GlobalPrefix"].Trim('/')}";
            BuildVersion = Configuration["BUILD_VERSION"] ?? string.Empty;
            BuildDate = Configuration["BUILD_DATE"] ?? string.Empty;
            AspNetCoreEnvironment = Configuration["ASPNETCORE_ENVIRONMENT"] ?? string.Empty;
            HostName = Configuration["HOSTNAME"] ?? string.Empty;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //services.AddCors();
            //+Front
            //services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            //{
            //    builder.AllowAnyOrigin()
            //           .AllowAnyMethod()
            //           .AllowAnyHeader();
            //}));


            //services.AddCors();

            //+Identity
            services.AddIdentity(Configuration);
            //-Identity

            services.AddCors(options =>
            {
                options.AddPolicy("Policy1", builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                    .WithMethods("POST", "GET", "PUT", "DELETE")
                    .WithHeaders(HeaderNames.ContentType)
                    .WithExposedHeaders("Content-Disposition");
                });
            });
            //-Front


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyNotes", Version = "v1" });
            });
            services.AddSwaggerExamplesFromAssemblyOf<Startup>();

            services.AddAutoMapper(cfg=>cfg.AddMaps(new[]
            {
                "MyNotes",
                "MyNotes.Services"
            }));

            services.AddDataAccess(Configuration);
            services.AddServiceLogic(Configuration);

            services.AddHealthChecks()
               .AddDbContextCheck<AppDbContext>()
               .AddCheck<MemoryHealthCheck>("memory")
               .AddCheck<ThreadPoolHealthCheck>("threadPool");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //+Front
            // app.UseCors("MyPolicy");
            //app.UseCors();
            //app.UseCors("Policy1");
            //-Front

            //Identity

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyNotes v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHealthChecks("/health", HealthCheckConfiguration.DefaultRules());
            app.UseHealthChecks("/health/full", HealthCheckConfiguration.FullRules());

            app.UseCors("Policy1");

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            //Serilog!
            // app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            //Inmemory
            PrepDb.PrepPopulation(app, env.IsProduction());
        }
    }
}
