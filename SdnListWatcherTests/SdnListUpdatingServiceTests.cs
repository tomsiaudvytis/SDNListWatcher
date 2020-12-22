using System.Text;
using Common.Enums;
using Common.Interfaces;
using Common.Models;
using Moq;
using SdnListWatcher.Services;
using Xunit;

namespace SdnListWatcherTests
{
    public class SdnListUpdatingServiceTests : FixtureBase<SdnListUpdatingService>
    {
        private readonly Mock<IContentDownloader> _contentDownloader;
        private readonly Mock<IXmlParser> _xmlParser;
        private readonly Mock<ISdnRepository> _sdnRepository;
        private readonly Mock<ILogger> _logger;

        public SdnListUpdatingServiceTests()
        {
            _contentDownloader = FreezeMock<IContentDownloader>();
            _xmlParser = FreezeMock<IXmlParser>();
            _sdnRepository = FreezeMock<ISdnRepository>();
            _logger = FreezeMock<ILogger>();

            Sut = CreateSut();
        }

        [Fact]
        public void DownloadAndStoreNewSdn_WhenDownloadFails_ShouldLogAndNotCallXmlParser()
        {
            _contentDownloader.Setup(x => x.DownloadUpdatedSdns())
                              .Returns(() => null);

            Sut.DownloadAndStoreNewSdn();

            _logger.Verify(x => x.Log(It.IsAny<string>(), It.IsAny<LogLevel>()), Times.Once);
        }

        [Fact]
        public void DownloadAndStoreNewSdn_WhenDownloadSucceeds_ShouldParseTryParseXml()
        {
            SdnList result = null;
            var xml = "aka xml";

            _contentDownloader.Setup(x => x.DownloadUpdatedSdns()).Returns(xml);

            Sut.DownloadAndStoreNewSdn();

            _xmlParser.Verify(x => x.TryDeserializeObject<SdnList>(out result, xml));
        }

        [Fact]
        public void DownloadAndStoreNewSdn_WhenParsingSucceeds_ShouldCallRepository()
        {
            var xml = "aka xml";
            var result = new SdnList();

            _contentDownloader.Setup(x => x.DownloadUpdatedSdns()).Returns(xml);
            _xmlParser.Setup(x => x.TryDeserializeObject<SdnList>(out result, xml)).Returns(() => true);

            Sut.DownloadAndStoreNewSdn();

            _sdnRepository.Verify(x => x.AddMany(It.IsAny<SdnList>()));
        }
    }
}