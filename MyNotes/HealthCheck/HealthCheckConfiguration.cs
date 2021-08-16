using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MyNotes;
using Newtonsoft.Json;

namespace RisGmp.Adapter.HealthCheck
{
    public static class HealthCheckConfiguration
    {
        public static HealthCheckOptions DefaultRules()
        {
            var options = new HealthCheckOptions
            {
                Predicate = _ => false
            };
            options.ResultStatusCodes[HealthStatus.Healthy] = StatusCodes.Status200OK;
            options.ResultStatusCodes[HealthStatus.Degraded] = StatusCodes.Status200OK;
            options.ResultStatusCodes[HealthStatus.Unhealthy] = StatusCodes.Status500InternalServerError;
            return options;
        }

        public static HealthCheckOptions FullRules()
        {
            var options = DefaultRules();
            options.Predicate = _ => true;
            options.ResponseWriter = ResponseWriter;
            return options;
        }

        private static Task ResponseWriter(HttpContext context, HealthReport report)
        {
            var subReports = report.Entries.Select(e => new
            {
                componentName = e.Key,
                checkResult = e.Value.Status.ToString(),
                e.Value.Duration.TotalMilliseconds,
                e.Value.Duration,
                e.Value.Data,
                e.Value.Exception?.Message
            }).ToArray();
            var result = new
            {
                checkResult = report.Status.ToString(),
                checkTime = DateTime.Now,
                componentChecks = subReports,
                buildVersion = Startup.BuildVersion,
                buildDate = Startup.BuildDate,
                aspNetCoreEnvironment = Startup.AspNetCoreEnvironment,
            };
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result, Formatting.Indented));
        }
    }
}
