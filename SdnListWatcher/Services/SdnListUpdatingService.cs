using Common.Enums;
using Common.Interfaces;
using Common.Models;

namespace SdnListWatcher.Services
{
    public class SdnListUpdatingService : ISdnListUpdatingService
    {
        private readonly IContentDownloader _contentDownloader;
        private readonly IXmlParser _xmlParser;
        private readonly ISdnRepository _sdnRepository;
        private readonly ILogger _logger;

        public SdnListUpdatingService(ISdnRepository sdnRepository,
                                      IXmlParser xmlParser,
                                      IContentDownloader contentDownloader,
                                      ILogger logger)
        {
            _sdnRepository = sdnRepository;
            _xmlParser = xmlParser;
            _contentDownloader = contentDownloader;
            _logger = logger;
        }

        public void DownloadAndStoreNewSdn()
        {
            var sdnSource = _contentDownloader.DownloadUpdatedSdns();

            if (sdnSource == null)
            {
                _logger.Log("SdnSource download failed", LogLevel.Error);
                return;
            }

            if (_xmlParser.TryDeserializeObject<SdnList>(out var updatedSdn, sdnSource))
            {
                _sdnRepository.AddMany(updatedSdn);
            }
        }
    }
}