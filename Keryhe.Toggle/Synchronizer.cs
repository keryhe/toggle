using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Keryhe.Toggle
{
    public class Synchronizer : BackgroundService
    {

        public Synchronizer()
        {
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //call feature flag service to populate features cache
            // then watch queue to retreive changes

            throw new NotImplementedException();
        }
    }
}
