using MfeClusterConfigurations.Application.Find;
using MfeGlobalConfigurations.Application.Update;
using MfeGlobalConfigurations.Domain;
using MfeTenantConfigurations.Application.Create;
using MfeTenantConfigurations.Application.Find;
using MfeTenantConfigurations.Application.Update;
using MfeTenantConfigurations.Application.UpdateActiveConfiguration;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using VersioningService.Core.Interfaces.Repositories;
//using VersioningService.Core.Interfaces.Services;
//using VersioningService.Core.Services;
//using VersioningService.Infrastructure.Context;
//using VersioningService.Infrastructure.Repositories;

namespace mfe_versions.api.Extensions.DependencyInjection
{
    /// <summary>
    /// Class for application level dependecy injections
    /// </summary>
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddScoped<MfeTenantConfigurationCreator>();
            services.AddScoped<MfeTenantConfigurationUpdator>();
            services.AddScoped<MfeActiveConfigurationUpdator>();
            services.AddScoped<MfeGlobalConfigurationUpdator>();
            services.AddScoped<MfeTenantConfigurationFinder>();
            services.AddScoped<MfeClusterConfigurationFinder>();

            //services.AddScoped<IMicrofrontEndService, MicrofrontEndService>();
            //services.AddScoped<IMicrofrontEndRepository, MicrofronEndRepository>();
            services.AddScoped<MfeGlobalConfigurationFinder>();//TODO: Remove after QueryBus usage?
            //services.AddHttpClient();
            return services;
        }
    }
}
