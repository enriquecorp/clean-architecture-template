namespace mfe_versions.api.Extensions.Environment
{
    public static class EnvironmentExtensions
    {
        public static bool IsLocal(this IWebHostEnvironment environment)
        {
            return environment.IsEnvironment("local");
        }

        public static bool IsSandbox(this IWebHostEnvironment environment)
        {
            return environment.IsEnvironment("sandbox");
        }

        public static bool IsIcDev(this IWebHostEnvironment environment)
        {
            return environment.IsEnvironment("ic-dev");
        }

        public static bool IsIcProd(this IWebHostEnvironment environment)
        {
            return environment.IsEnvironment("ic-prod");
        }

        public static bool IsCloud(this IWebHostEnvironment environment)
        {
            return !environment.IsEnvironment("local");
        }
    }
}
