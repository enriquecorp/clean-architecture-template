namespace mfe_versions.api.Extensions
{
    public static class Startup
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            //services.ConfigureCors();

            //services.ConfigureDependencyInjection(Configuration);

            //services.AddControllers();

            ////services.ConfigureSwagger();
            //services.ConfigureSwagger2();

            //services.AddAutoMapper(typeof(Startup));

            //services.AddRouting(options => options.LowercaseUrls = true);

            //services.ConfigureHealthChecks(Configuration);

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
