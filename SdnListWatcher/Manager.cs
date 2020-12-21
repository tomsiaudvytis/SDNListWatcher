using Common.Enums;
using Common.Interfaces;
using Common.Models;

namespace SdnListWatcher
{
    public class Manager : IManager
    {
        private readonly ISdnListMonitoringService _sdnListMonitoringService;
        private readonly ISdnListUpdatingService _sdnListUpdatingService;
        private readonly ISdnItemsProcessingService _sdnItemsProcessingService;
        private readonly ILogger _logger;

        public Manager(ISdnListUpdatingService sdnListUpdatingService,
                       ISdnListMonitoringService sdnListMonitoringService,
                       ISdnItemsProcessingService sdnItemsProcessingService,
                       ILogger logger)
        {
            _sdnListUpdatingService = sdnListUpdatingService;
            _sdnListMonitoringService = sdnListMonitoringService;
            _sdnItemsProcessingService = sdnItemsProcessingService;
            _logger = logger;
        }

        public void StartMonitoring()
        {
            _logger.Log("StartMonitoring() => Application started", LogLevel.Information);

            if (!SdnListBeenUpdated(out var updatedItems))
            {
                _logger.Log("No SDN update detected, exiting", LogLevel.Information);
                return;
            }

            UpdateDatabaseWithNewEsd();
            ProcessSdnItemsUpdate(updatedItems);

            _logger.Log("Application finished, existing", LogLevel.Information);
        }

        private void ProcessSdnItemsUpdate(OfacFeedSubscription ofacFeedSubscription)
            => _sdnItemsProcessingService.ProcessSdnUpdate(ofacFeedSubscription);

        private void UpdateDatabaseWithNewEsd()
            => _sdnListUpdatingService.UpdateDatabaseWithNewEsds();

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