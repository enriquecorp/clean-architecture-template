using mfe_versions.api.Extensions.DependencyInjection;

namespace mfe_versions.api.Extensions
{
    public static class Startup
    {
        public const string CorsPolicyName = "CorsPolicy";

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureCors();

            //services.ConfigureDependencyInjection(Configuration);

            ////services.ConfigureSwagger();
            //services.ConfigureSwagger2();

            //services.AddAutoMapper(typeof(Startup));

            //services.AddRouting(options => options.LowercaseUrls = true);

            //services.ConfigureHealthChecks(Configuration);

            services.AddControllers();
            services.AddApplicationDependencies();
            services.AddInfrastructureDependencies(configuration);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        private static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }
    }
}
