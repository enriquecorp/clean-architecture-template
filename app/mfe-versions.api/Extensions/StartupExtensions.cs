using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using mfe_versions.api.Extensions.DependencyInjection;
using mfe_versions.api.Extensions.HealthCheck;
using mfe_versions.api.Extensions.Middlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
//using VersioningService.HealthChecks;
//using VersioningService.Middlewares;

namespace mfe_versions.api.Extensions
{
    public static class StartupExtensions
    {
        public const string CorsPolicyName = "CorsPolicy";

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureCors();

            services.ConfigureDependencyInjection(configuration);

            services.AddControllers();

            services.ConfigureSwagger();

            //services.AddAutoMapper(typeof(Startup));

            services.AddRouting(options => options.LowercaseUrls = true);

            services.ConfigureHealthChecks(configuration);
        }

        public static void UseServices(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsLocal())
            {
                // Enable dev html responses for errors.
                app.UseDeveloperExceptionPage();
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                app.UseAppSwagger(provider);
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }
            if (!app.Environment.IsLocal()) // Cloud environment
            {
                app.UseHttpStatusCodeMiddleware();
                app.UseHsts();// Strict https transport header
            }

            app.UseHttpsRedirection();
            app.UseRouting(); // This is required by Health check UI configuration

            app.UseAuthorization();

            app.UseMapControllers();
        }

        private static void UseMapControllers(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // endpoints.MapHealthChecks("/health");
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                app.UseHealthChecksUI(delegate (Options options)
                {
                    options.UIPath = "/healthcheck-ui";
                    options.AddCustomStylesheet($"{AppContext.BaseDirectory}/extensions/HealthCheck/custom.css");
                });
                // Health Cheks recommended here: https://tlvconfluence01.nice.com/display/IN/GEN+ADR4:+Health+Check+Endpoints
                // endpoints.MapHealthChecks("/health", new HealthCheckOptions() { Predicate = (_) => false });
                endpoints.MapHealthChecks("/healthcheck", new HealthCheckOptions() { Predicate = (_) => false });
                endpoints.MapHealthChecks("/probe/healthcheck", new HealthCheckOptions() { Predicate = (_) => false });
                endpoints.MapHealthChecks("/probe/host", new HealthCheckOptions() { Predicate = (check) => check.Tags.Contains("host"), ResponseWriter = ResponseWritters.HostProbeWriter });
                endpoints.MapHealthChecks("/activecheck", new HealthCheckOptions() { Predicate = (check) => check.Tags.Contains("host"), ResponseWriter = ResponseWritters.HostProbeWriter });
                endpoints.MapHealthChecks("/probe/ready", new HealthCheckOptions() { Predicate = (check) => check.Tags.Contains("ready") });
                endpoints.MapHealthChecks("/probe/healthreport", new HealthCheckOptions() { Predicate = (check) => check.Tags.Contains("ready"), ResponseWriter = ResponseWritters.HealthReportWriter });
            });
        }

        private static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(CorsPolicyName, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
        }

        private static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDependencies();
            services.AddInfrastructureDependencies(configuration);
        }

        private static void ConfigureSwagger(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();
            services.ConfigureSwaggerServices();
        }

    }
}
