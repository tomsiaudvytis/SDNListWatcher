using Common.Models;

namespace Common.Interfaces
{
    public interface ISdnItemsProcessingService
    {
        void ProcessNewestSdnItems(OfacFeedSubscription ofacFeedSubscription);
    }
}