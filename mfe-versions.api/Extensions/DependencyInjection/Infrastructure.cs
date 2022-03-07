using System;

namespace mfe_versions.api.Extensions.DependencyInjection
{
    public static class Infrastructure
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

            return services;
        }
    }
    
}
