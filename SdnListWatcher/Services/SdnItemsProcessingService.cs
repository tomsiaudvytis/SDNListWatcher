using System;
using System.Collections;
using System.Text.RegularExpressions;
using Common;
using Common.Enums;
using Common.Interfaces;
using Common.Models;
using SdnListWatcher.Extensions;

namespace SdnListWatcher.Services
{
    public class SdnItemsProcessingService : ISdnItemsProcessingService
    {
        private readonly IContentDownloader _contentDownloader;
        private readonly IMockDatabase _mockDatabase;
        private readonly IContentParser _contentParser;
        private readonly ILogger _logger;

        public SdnItemsProcessingService(IMockDatabase mockDatabase,
                                         IContentDownloader contentDownloader,
                                         IContentParser contentParser,
                                         ILogger logger)
        {
            _mockDatabase = mockDatabase;
            _contentDownloader = contentDownloader;
            _contentParser = contentParser;
            _logger = logger;
        }

        public void ProcessSdnUpdate(OfacFeedSubscription ofacFeedSubscription)
        {
            for (var i = ofacFeedSubscription.Channel.Items.Count - 1; i >= 0; i--)
            {
                var item = ofacFeedSubscription.Channel.Items[i];

                var publicationDate = item.PubDate.ParsePublicationDate();
                if (publicationDate == null || !(publicationDate > _mockDatabase.GetLastUpdateDateTime()))
                    continue;

                var updatedSource = _contentDownloader.DownloadContent(item.Link);

                var addedSdnItems = _contentParser.TryExtractSdnItems(updatedSource, SdnChangeType.Add);
                ExtractAndStoreUpdatedSdnItems(addedSdnItems, (DateTime) publicationDate, SdnChangeType.Add);

                var updatedSdnItems = _contentParser.TryExtractSdnItems(updatedSource, SdnChangeType.Update);
                ExtractAndStoreUpdatedSdnItems(updatedSdnItems, (DateTime) publicationDate, SdnChangeType.Update);

                var removedSdnItems = _contentParser.TryExtractSdnItems(updatedSource, SdnChangeType.Remove);
                ExtractAndStoreUpdatedSdnItems(removedSdnItems, (DateTime) publicationDate, SdnChangeType.Remove);
            }
        }

        private void ExtractAndStoreUpdatedSdnItems(ICollection matchCollection,
                                                    DateTime sdnPublicationDate,
                                                    SdnChangeType sdnChangeType)
        {
            if (matchCollection.Count == 0)
                return;

            foreach (Match collection in matchCollection)
            {
                var sdnCollectionMatches = _contentParser.TryExtractLastName(collection.Value);

                if (sdnCollectionMatches.Count == 0)
                {
                    _logger.Log($"{collection.Value} extraction failed", LogLevel.Error);
                    continue;
                }

                foreach (Match sdnCollectionMatch in sdnCollectionMatches)
                {
                    if (!sdnCollectionMatch.Success)
                    {
                        _logger.Log($"{sdnCollectionMatch.Value} extraction failed", LogLevel.Error);
                        continue;
                    }

                    var cleanName = sdnCollectionMatch.Value.CleanSdnName();
                    var sdnEntry = _mockDatabase.GetEntryByLastName(cleanName);
                    sdnEntry.SdnChangeType = sdnChangeType;
                    _mockDatabase.AddSdnRecordToHistory(sdnPublicationDate, sdnEntry);
                }
            }
        }
    }
}