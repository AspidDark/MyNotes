using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyNotes.HealthCheck
{
    public class MemoryHealthCheck : IHealthCheck
    {
        private readonly IOptionsMonitor<MemoryCheckOptions> _options;

        public MemoryHealthCheck(IOptionsMonitor<MemoryCheckOptions> options)
        {
            _options = options;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var options = _options.CurrentValue;
            var allocated = GC.GetTotalMemory(false);
            var data = new Dictionary<string, object>
            {
                {"AllocatedBytes", allocated.ToString()},
                {"Gen0Collections", GC.CollectionCount(0).ToString()},
                {"Gen1Collections", GC.CollectionCount(1).ToString()},
                {"Gen2Collections", GC.CollectionCount(2).ToString()},
            };
            var status = allocated < options.Threshold
                ? HealthStatus.Healthy
                : HealthStatus.Degraded;

            return Task.FromResult(new HealthCheckResult(
                status,
                $"Reports degraded status if allocated bytes >= {options.Threshold} bytes.",
                null,
                data));
        }
    }

    public class MemoryCheckOptions
    {
        public long Threshold { get; set; } = 1024 * 1024 * 1024;
    }
}
