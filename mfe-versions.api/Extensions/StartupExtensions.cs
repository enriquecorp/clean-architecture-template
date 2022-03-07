using mfe_versions.api.Extensions.DependencyInjection;
using mfe_versions.api.Extensions.HealthCheck;
using mfe_versions.api.Extensions.Middlewares;

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

        public static void UseServices (this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Enable dev html responses for errors.
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            if (!app.Environment.IsDevelopment()) // Prod environment
            {
                app.UseHttpStatusCodeMiddleware();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }

        private static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }

        private static void ConfigureDependencyInjection(this IServiceCollection services,IConfiguration configuration)
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
