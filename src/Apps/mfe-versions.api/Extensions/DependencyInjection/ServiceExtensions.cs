using Versioning.Domain.GlobalConfigurations;
using Versioning.Service.ClusterConfigurations.Find;
using Versioning.Service.GlobalConfigurations.Update;
using Versioning.Service.TenantConfigurations.Create;
using Versioning.Service.TenantConfigurations.Find;
using Versioning.Service.TenantConfigurations.Update;
using Versioning.Service.TenantConfigurations.UpdateActiveConfiguration;

namespace mfe_versions.api.Extensions.DependencyInjection
{
    /// <summary>
    /// Class for application level dependecy injections
    /// </summary>
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddScoped<TenantConfigurationCreator>();
            services.AddScoped<TenantConfigurationUpdater>();
            services.AddScoped<ActiveConfigurationUpdater>();
            services.AddScoped<GlobalConfigurationUpdater>();
            services.AddScoped<TenantConfigurationFinder>();
            services.AddScoped<ClusterConfigurationFinder>();
            services.AddScoped<GlobalConfigurationFinder>(); //TODO: Remove after QueryBus usage?

            //services.AddScoped<IMicrofrontEndService, MicrofrontEndService>();
            //services.AddScoped<IMicrofrontEndRepository, MicrofronEndRepository>();

            //services.AddHttpClient();
            return services;
        }
    }
}
