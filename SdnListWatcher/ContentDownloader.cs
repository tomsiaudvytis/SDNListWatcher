using Common.Configurations;
using Common.Interfaces;
using Microsoft.Extensions.Options;

namespace SdnListWatcher
{
    public class ContentDownloader : IContentDownloader
    {
        private readonly UrlSettings _urlSettings;
        private readonly IRestClientWrapper _restClientWrapper;

        public ContentDownloader(IOptions<UrlSettings> urlSettings, IRestClientWrapper restClientWrapper)
        {
            _urlSettings = urlSettings.Value;
            _restClientWrapper = restClientWrapper;
        }

        public string DownloadOfacSubscriptionPageContent()
        {
            return _restClientWrapper.Get(_urlSettings.OfacUrl);
        }

        public string DownloadUpdatedSdns()
        {
            return _restClientWrapper.Get(_urlSettings.SdnFeedUrl);
        }

        public string DownloadContent(string link)
        {
            return _restClientWrapper.Get(link);
        }
    }
}