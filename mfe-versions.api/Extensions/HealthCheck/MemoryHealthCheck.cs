using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace mfe_versions.api.Extensions.HealthCheck
{
    public class MemoryHealthCheck : IHealthCheck
    {
        private readonly IOptionsMonitor<MemoryCheckOptions> options;

        private string Name => "memory_check";

        public MemoryHealthCheck(IOptionsMonitor<MemoryCheckOptions> options)
        {
            this.options = options;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var options = this.options.Get(context.Registration.Name);

            //Include Garbage Collection Information in the reported diagnostics
            var allocated = GC.GetTotalMemory(forceFullCollection: true);
            var data = new Dictionary<string, object>(){
                {"AllocatedBytes", allocated},
                {"Gen0Collections", GC.CollectionCount(0)},
                {"Gen1Collections", GC.CollectionCount(1)},
                {"Gen2Collections", GC.CollectionCount(2)}
            };
            var status = (allocated < options.Threshold) ? HealthStatus.Healthy : HealthStatus.Unhealthy;
            // return Task.FromResult(new HealthCheckResult(status, description: "Reports degraded status if", exception: null, data: data));
            return await Task.Run(()=> new HealthCheckResult(status, description: $"Reports degraded status if allocated bytes >= {options.Threshold} bytes.", exception: null, data: data));
            // return new HealthCheckResult(status, description: $"Reports degraded status if allocated bytes >= {options.Threshold} bytes.", exception: null, data: data);
        }
    }

    public class MemoryCheckOptions
    {
        public string MemoryStatus { get; set; } = string.Empty;
        public long Threshold { get; set; } = 1024L * 1024L * 1024L;
    }
}
