namespace mfe_versions.api.Extensions
{
    public static class HostExtensions
    {
        public static void ConfigureHost(this ConfigureHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var hostEnvironment = hostingContext.HostingEnvironment;
                config.SetBasePath(hostEnvironment.ContentRootPath);
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                //if (Environment.GetEnvironmentVariable("RUNNING_IN_PIPELINE") == "true") //DOTNET_RUNNING_IN_CONTAINER
                //{
                //    config.AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}-pipeline.json", optional: true, reloadOnChange: true);
                //}
                config.AddEnvironmentVariables();
            });
        }
    }
}
