using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keryhe.Toggle;
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

                services.AddHttpClient<IFeature, Feature>(f =>
                {
                    f.BaseAddress = new Uri("http://localhost:6000");
                });

                services.AddHostedService<Worker>();
            });
        }
    }
}
