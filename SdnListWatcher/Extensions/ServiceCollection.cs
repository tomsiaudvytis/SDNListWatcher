using System.IO;
using Common.Configurations;
using Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SdnListWatcher.Services;

namespace SdnListWatcher.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection RegisterSdnListWatcher(this IServiceCollection services)
        {
            return services.AddSingleton<IManager, Manager>()
                    .AddSingleton<IContentDownloader, ContentDownloader>()
                    .AddSingleton<IXmlParser, XmlParser>()
                    .AddSingleton<ISdnListMonitoringService, SdnListMonitoringService>()
                    .AddSingleton<ISdnListUpdatingService, SdnListUpdatingService>()
                    .AddSingleton<ISdnItemsProcessingService, SdnItemsProcessingService>()
                    .AddSingleton<IContentParser, ContentParser>()
                    .AddSingleton<ILogger, Logger.Logger>()
                    .AddSingleton<IMockDatabase, MockDatabase>();

            // var configuration = new ConfigurationBuilder()
            //                     .SetBasePath(Directory.GetCurrentDirectory())
            //                     .AddJsonFile(appSettingsPath)
            //                     .Build();
            //
            // services.AddOptions();
            // services.Configure<RegexConfigurationPattern>(configuration.GetSection("RegexOptions"));
            // services.Configure<UrlSettings>(configuration.GetSection("UrlSettings"));

            return services;
        }
    }
}