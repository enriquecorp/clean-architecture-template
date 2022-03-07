using System;

namespace mfe_versions.api.Extensions.DependencyInjection
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            //var appSettings = configuration.GetSection("AppSettings").Get<AppSettingsSection>();
            //string connectionString = string.Empty;
            //if ((bool)appSettings?.ByPassKeyVault)
            //{
            //    connectionString = configuration.GetConnectionString("versioningdb");
            //}
            //else
            //{
            //    connectionString = GetSecret.VersioningConnectionString();
            //}
            ////NOT for production
            //services.AddDbContext<VersioningDbContext>(opts => opts.UseInMemoryDatabase("MemInDB")); // This is just a workaround for using in-memory storage temporaly
            //// services.AddDbContext<VersioningDbContext>(optionsAction: opt => opt.UseSqlServer(connectionString), contextLifetime: ServiceLifetime.Transient, optionsLifetime: ServiceLifetime.Transient);

            return services;
        }
    }
    
}
