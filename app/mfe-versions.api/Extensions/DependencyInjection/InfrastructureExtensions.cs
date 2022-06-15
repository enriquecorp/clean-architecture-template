using Amazon.DynamoDBv2;
using MfeClusterConfigurations.Domain;
using MfeClusterConfigurations.Infrastructure.Persistence;
using MfeGlobalConfigurations.Domain;
using MfeGlobalConfigurations.Infrastructure.Persistence;
using MfeTenantConfigurations.Domain;
using MfeTenantConfigurations.Infrastructure.Persistence;
using shared.domain.Bus.Event;
using Shared.Infrastructure.Bus.Event;

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

            services.AddDynamoDb(configuration);
            services.AddScoped<ITenantConfigurationRepository, InMemoryTenantConfigurationRepository>();
            //services.AddScoped<IClusterConfigurationRepository, InMemoryClusterConfigurationRepository>();
            services.AddScoped<IClusterConfigurationRepository, DynamoDbClusterConfigurationRepository>();
            //services.AddScoped<IGlobalConfigurationRepository, InMemoryGlobalConfigurationRepository>();
            services.AddScoped<IGlobalConfigurationRepository, DynamoDbGlobalConfigurationRepository>();
            services.AddScoped<IEventBus, InMemoryApplicationEventBus>();


            return services;
        }

        private static void AddDynamoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var dynamoDbSection = configuration.GetSection("DynamoDb");
            var localMode = dynamoDbSection.GetValue<bool>("localMode");
            if (localMode)
            {
                services.AddSingleton<IAmazonDynamoDB>(serviceProvider =>
                {
                    var clientConfig = new AmazonDynamoDBConfig { ServiceURL = dynamoDbSection.GetValue<string>("localServiceUrl") };
                    //clientConfig.RegionEndpoint = Amazon.RegionEndpoint.USWest2;
                    return new AmazonDynamoDBClient(clientConfig);
                });
            }
            else
            {
                services.AddSingleton<IAmazonDynamoDB>(new AmazonDynamoDBClient());
                //services.AddAWSService<IAmazonDynamoDB>()
            }
        }
    }

}
