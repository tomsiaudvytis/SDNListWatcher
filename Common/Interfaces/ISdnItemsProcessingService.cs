using Common.Models;

namespace Common.Interfaces
{
    public interface ISdnItemsProcessingService
    {
        void ProcessSdnUpdate(OfacFeedSubscription ofacFeedSubscription);
    }
}