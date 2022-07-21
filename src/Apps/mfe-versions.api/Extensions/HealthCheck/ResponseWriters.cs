using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Environment;

namespace mfe_versions.api.Extensions.HealthCheck
{
    public static class ResponseWriters
    {
        public static Task HostProbeWriter(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "text/plain";
            // TODO: at least when running in the IDE, result.Entries["host"] is not defined
            ;
            return context.Response.WriteAsync(result.Entries.GetValueOrDefault("host").Description ?? string.Empty);
        }

        public static Task HealthReportWriter(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "application/json";

            var json = new JObject(
                new JProperty("Service", "Versioning API"),
                new JProperty("Hostname", MachineName),
                new JProperty("Result", result.Status.ToString()),
                new JProperty("Timestamp", DateTime.UtcNow.ToString("o")),
                new JProperty("Tests", new JObject(result.Entries.Select(pair =>
                    new JProperty(pair.Key, new JObject(
                        new JProperty("Result", pair.Value.Status.ToString()),
                        new JProperty("Description", pair.Value.Description),
                        new JProperty("Data", new JObject(pair.Value.Data.Select(
                            p => new JProperty(p.Key, p.Value))))))))));

            return context.Response.WriteAsync(
                json.ToString(Formatting.Indented));
        }
    }
}
