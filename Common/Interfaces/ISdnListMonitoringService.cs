using Common.Models;

namespace Common.Interfaces
{
    public interface ISdnListMonitoringService
    {
        OfacFeedSubscription GetLatestSdnSubscription();
        bool SdnListBeenUpdated(OfacFeedSubscription result);
    }
}