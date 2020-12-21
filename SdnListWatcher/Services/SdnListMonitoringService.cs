using System;
using System.Globalization;
using Common.Interfaces;
using Common.Models;

namespace SdnListWatcher.Services
{
    public class SdnListMonitoringService : ISdnListMonitoringService
    {
        private readonly IContentDownloader _contentDownloader;
        private readonly IXmlParser _xmlParser;
        private readonly IMockDatabase _mockDatabase;

        public SdnListMonitoringService(IMockDatabase mockDatabase,
                                        IXmlParser xmlParser,
                                        IContentDownloader contentDownloader)
        {
            _mockDatabase = mockDatabase;
            _xmlParser = xmlParser;
            _contentDownloader = contentDownloader;
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
            var lastPublicationDate = ParsePublicationDate(ofacFeedSubscription.Channel.PubDate);

            if (!lastPublicationDate.HasValue)
            {
                // log ....
                return false;
            }

            return _mockDatabase.GetLastUpdateDateTime() < lastPublicationDate;
        }

        private DateTime? ParsePublicationDate(string date)
        {
            return DateTime.TryParseExact(date, "dd MMM yyyy HH:mm:ss EDT", null, DateTimeStyles.None, out var result)
                ? result
                : (DateTime?) null;
        }
    }
}