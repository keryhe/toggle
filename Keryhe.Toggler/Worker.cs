using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Keryhe.Toggle;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Keryhe.Toggler
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IFeature _feature;

        public Worker(ILogger<Worker> logger, IFeature feature)
        {
            _logger = logger;
            _feature = feature;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                if(await _feature.IsOnAsync("Test"))
                {
                    _logger.LogInformation("Test is On");
                }
                else
                {
                    _logger.LogInformation("Test is Off");
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
