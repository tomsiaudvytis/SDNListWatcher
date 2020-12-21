using Common.Configurations;
using Common.Interfaces;
using Microsoft.Extensions.Options;
using RestSharp;

namespace SdnListWatcher
{
    public class ContentDownloader : IContentDownloader
    {
        private readonly IRestClient _client;
        private readonly UrlSettings _urlSettings;

        public ContentDownloader(IOptions<UrlSettings> urlSettings)
        {
            _client = new RestClient();
            _urlSettings = urlSettings.Value;
        }

        public string DownloadOfacSubscriptionPageContent()
        {
            return Get(_urlSettings.OfacUrl);
        }

        public string DownloadUpdatedSdns()
        {
            return Get(_urlSettings.SdnFeedUrl);
        }

        public string DownloadContent(string link)
        {
            return Get(link);
        }

        private string Get(string url)
        {
            var request = new RestRequest(url);
            var response = _client.Execute(request);

            return response?.Content;
        }
    }
}