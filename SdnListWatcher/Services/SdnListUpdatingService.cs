using Common.Interfaces;
using Common.Models;

namespace SdnListWatcher.Services
{
    public class SdnListUpdatingService : ISdnListUpdatingService
    {
        private readonly IContentDownloader _contentDownloader;
        private readonly IXmlParser _xmlParser;
        private readonly IMockDatabase _mockDatabase;

        public SdnListUpdatingService(IMockDatabase mockDatabase,
                                      IXmlParser xmlParser,
                                      IContentDownloader contentDownloader)
        {
            _mockDatabase = mockDatabase;
            _xmlParser = xmlParser;
            _contentDownloader = contentDownloader;
        }

        public void UpdateDatabaseWithNewEsds()
        {
            var sdnSource = _contentDownloader.DownloadUpdatedSdns();

            if (_xmlParser.TryDeserializeObject<SdnList>(out var updatedSdn, sdnSource))
            {
                _mockDatabase.UpdateExistingSdnItems(updatedSdn);
            }
        }
    }
}