using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keryhe.Toggle;
using Keryhe.Toggle.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Keryhe.Toggler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDistributedMemoryCache();

                services.AddSingleton<IConnectionFactory, ConnectionFactory>();
                services.AddTransient<IToggleRepo, ToggleRepo>();
                services.AddTransient<IFeature, Feature>();

                services.AddHostedService<Worker>();
            });
        }
    }
}
