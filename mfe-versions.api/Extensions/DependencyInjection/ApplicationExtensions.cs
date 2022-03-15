using System;
using MfeConfigurations.Application.Create;
using MfeConfigurations.Application.Find;
using MfeConfigurations.Application.Update;
using MfeConfigurations.Application.UpdateActiveConfiguration;
using MfeConfigurations.Domain;
using MfeConfigurations.Infrastructure;
using MfeGlobalConfigurations.Application.Update;
using MfeGlobalConfigurations.Domain;
using MfeGlobalConfigurations.Infrastructure;
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

            services.AddScoped<IMfeTenantConfigurationRepository, MfeConfigurationInMemoryRepository>();
            services.AddScoped<IMfeGlobalConfigurationRepository, MfeGlobalConfigurationInMemoryRepository>();
            //services.AddScoped<IMicrofrontEndService, MicrofrontEndService>();
            //services.AddScoped<IMicrofrontEndRepository, MicrofronEndRepository>();
            //services.AddHttpClient();
            return services;
        }
    }
}
