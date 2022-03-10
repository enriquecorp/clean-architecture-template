using System;
using MfeConfigurations.Application.Create;
using MfeConfigurations.Domain;
using MfeConfigurations.Infrastructure;
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
            services.AddScoped<MfeConfigurationCreator>();
            services.AddScoped<IMfeConfigurationRepository, MfeConfigurationInMemoryRepository>();
            //services.AddScoped<IMicrofrontEndService, MicrofrontEndService>();
            //services.AddScoped<IMicrofrontEndRepository, MicrofronEndRepository>();
            //services.AddHttpClient();
            return services;
        }
    }
}
