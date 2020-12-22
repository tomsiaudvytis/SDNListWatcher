using Common.Enums;
using Common.Interfaces;
using Common.Models;

namespace SdnListWatcher
{
    public class SdnListWatcher : ISdnListWatcher
    {
        private readonly ISdnListMonitoringService _sdnListMonitoringService;
        private readonly ISdnListUpdatingService _sdnListUpdatingService;
        private readonly ISdnItemsProcessingService _sdnItemsProcessingService;
        private readonly ILogger _logger;

        public SdnListWatcher(ISdnListUpdatingService sdnListUpdatingService,
                              ISdnListMonitoringService sdnListMonitoringService,
                              ISdnItemsProcessingService sdnItemsProcessingService,
                              ILogger logger)
        {
            _sdnListUpdatingService = sdnListUpdatingService;
            _sdnListMonitoringService = sdnListMonitoringService;
            _sdnItemsProcessingService = sdnItemsProcessingService;
            _logger = logger;
        }

        public void RunSingleCheck()
        {
            _logger.Log("StartMonitoring() => Application started", LogLevel.Information);

            if (!SdnListBeenUpdated(out var updatedItems))
            {
                _logger.Log("No SDN update detected, exiting", LogLevel.Information);
                return;
            }

            DownloadAndStoreNewSdn();
            ProcessNewestSdnItems(updatedItems);

            _logger.Log("Application finished, existing", LogLevel.Information);
        }

        private void ProcessNewestSdnItems(OfacFeedSubscription ofacFeedSubscription)
            => _sdnItemsProcessingService.ProcessNewestSdnItems(ofacFeedSubscription);

        private void DownloadAndStoreNewSdn()
            => _sdnListUpdatingService.DownloadAndStoreNewSdn();

        private bool SdnListBeenUpdated(out OfacFeedSubscription ofacFeedSubscription)
        {
            ofacFeedSubscription = _sdnListMonitoringService.GetLatestSdnSubscription();

            if (ofacFeedSubscription == null)
            {
                _logger.Log("ofacFeedSubscription extraction failed", LogLevel.Fatal);
                return false;
            }

            return _sdnListMonitoringService.SdnListBeenUpdated(ofacFeedSubscription);
        }
    }
}