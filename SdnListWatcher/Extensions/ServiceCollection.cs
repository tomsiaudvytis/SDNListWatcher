using Common.Configurations;
using Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SdnListWatcher.Services;

namespace SdnListWatcher.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterSdnListWatcher(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSingleton<ISdnListWatcher, SdnListWatcher>()
                           .AddSingleton<IContentDownloader, ContentDownloader>()
                           .AddSingleton<IXmlParser, XmlParser>()
                           .AddSingleton<ISdnListMonitoringService, SdnListMonitoringService>()
                           .AddSingleton<ISdnListUpdatingService, SdnListUpdatingService>()
                           .AddSingleton<ISdnItemsProcessingService, SdnItemsProcessingService>()
                           .AddSingleton<IContentParser, ContentParser>()
                           .AddSingleton<ILogger, Logger.Logger>()
                           .AddSingleton<ISdnRepository, SdnRepository>()
                           .AddSingleton<IRegexes, Regexes>()
                           .AddSingleton<ISdnStorage, SdnStorage>()
                           .AddSingleton<IRestClientWrapper, RestClientWrapper>()
                           .Configure<RegexConfiguration>(options => configuration.GetSection("RegexOptions").Bind(options))
                           .Configure<LoggingSettings>(options => configuration.GetSection("LoggingSettings").Bind(options))
                           .Configure<UrlSettings>(options => configuration.GetSection("UrlSettings").Bind(options));
        }
    }
} 