using Common.Interfaces;
using RestSharp;

namespace SdnListWatcher
{
    public class RestClientWrapper : IRestClientWrapper
    {
        private readonly IRestClient _client;

        public RestClientWrapper() => _client = new RestClient();

        public string Get(string url)
        {
            var request = new RestRequest(url);
            var response = _client.Execute(request);

            return response?.Content;
        }
    }
}