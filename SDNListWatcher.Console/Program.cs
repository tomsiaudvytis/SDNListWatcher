using System;
using Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace SDNListWatcher.Console
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            ConfigureServices();
            var manager = _serviceProvider.GetService<IManager>();
            manager?.StartMonitoring();
        }

        public static void ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            SdnListWatcher.Extensions.ServiceCollection.RegisterSdnListWatcher(services)

            
        }
    }
}