using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyNotes.HealthCheck
{
    public class ThreadPoolHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            // thread pool
            ThreadPool.GetAvailableThreads(out var availableWorkerThreads, out var availableIoThreads);
            ThreadPool.GetMaxThreads(out var maxWorkerThreads, out var maxIoThreads);

            var data = new Dictionary<string, object>
            {
                {"availableWorkerThreads", availableWorkerThreads.ToString()},
                {"availableIoThreads", availableIoThreads.ToString()},
                {"maxWorkerThreads", maxWorkerThreads.ToString()},
                {"maxIoThreads", maxIoThreads.ToString()},
            };

            return Task.FromResult(new HealthCheckResult(
                HealthStatus.Healthy,
                $"Thread pool status: availableWorkerThreads {availableWorkerThreads} availableIoThreads {availableIoThreads} maxWorkerThreads {maxWorkerThreads} maxIoThreads {maxIoThreads}",
                null,
                data));
        }
    }
}
