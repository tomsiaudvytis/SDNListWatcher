using System;
using System.Globalization;
using Common.Enums;
using Common.Interfaces;
using Common.Models;
using SdnListWatcher.Extensions;

namespace SdnListWatcher.Services
{
    public class SdnListMonitoringService : ISdnListMonitoringService
    {
        private readonly IContentDownloader _contentDownloader;
        private readonly IXmlParser _xmlParser;
        private readonly ISdnRepository _sdnRepository;
        private readonly ILogger _logger;

        public SdnListMonitoringService(ISdnRepository sdnRepository,
                                        IXmlParser xmlParser,
                                        IContentDownloader contentDownloader,
                                        ILogger logger)
        {
            _sdnRepository = sdnRepository;
            _xmlParser = xmlParser;
            _contentDownloader = contentDownloader;
            _logger = logger;
        }

        public OfacFeedSubscription GetLatestSdnSubscription()
        {
            var content = _contentDownloader.DownloadOfacSubscriptionPageContent();

            return _xmlParser.TryDeserializeObject<OfacFeedSubscription>(out var ofacFeedSubscription, content)
                ? ofacFeedSubscription
                : null;
        }

        public bool SdnListBeenUpdated(OfacFeedSubscription ofacFeedSubscription)
        {
            var lastPublicationDate = ofacFeedSubscription.Channel.PubDate.ParsePublicationDate();

            if (!lastPublicationDate.HasValue)
            {
                _logger.Log("lastPublicationDate parsing failed", LogLevel.Error);
                return false;
            }

            return _sdnRepository.GetLastUpdateDateTime() < lastPublicationDate;
        }
    }
}