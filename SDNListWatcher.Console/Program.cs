using System;
using System.IO;
using Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SdnListWatcher.Extensions;

namespace SDNListWatcher.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appSettings.json")
                                    .Build();

                var serviceProvider = ConfigureServices(configuration);
                var watcher = serviceProvider.GetService<ISdnListWatcher>();

                // add watcher to timer run every some time
                watcher?.RunSingleCheck();
            }
            catch (Exception)
            {
                // log to event viewer
            }
        }

        private static IServiceProvider ConfigureServices(IConfiguration configuration)
            => new ServiceCollection()
               .RegisterSdnListWatcher(configuration)
               .BuildServiceProvider();
    }
}