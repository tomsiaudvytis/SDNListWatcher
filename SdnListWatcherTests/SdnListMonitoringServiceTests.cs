using System;
using Common.Enums;
using Common.Interfaces;
using Common.Models;
using Moq;
using SdnListWatcher.Services;
using Xunit;

namespace SdnListWatcherTests
{
    public class SdnListMonitoringServiceTests : FixtureBase<SdnListMonitoringService>
    {
        private readonly Mock<IContentDownloader> _contentDownloader;
        private readonly Mock<IXmlParser> _xmlParser;
        private readonly Mock<ISdnRepository> _sdnRepository;
        private readonly Mock<ILogger> _logger;

        public SdnListMonitoringServiceTests()
        {
            _contentDownloader = FreezeMock<IContentDownloader>();
            _xmlParser = FreezeMock<IXmlParser>();
            _sdnRepository = FreezeMock<ISdnRepository>();
            _logger = FreezeMock<ILogger>();

            Sut = CreateSut();
        }

        [Fact]
        public void GetLatestSdnSubscription_WhenCalled_ShouldDownloadOfacFeed()
        {
            Sut.GetLatestSdnSubscription();

            _contentDownloader.Verify(x => x.DownloadOfacSubscriptionPageContent());
        }

        [Fact]
        public void GetLatestSdnSubscription_WhenFeedParsingFails_ShouldReturnNull()
        {
            _contentDownloader.Setup(x => x.DownloadOfacSubscriptionPageContent())
                              .Returns(() => "random thing to fail");

            var result = Sut.GetLatestSdnSubscription();

            Assert.Null(result);
        }

        [Fact]
        public void SdnListBeenUpdated_WhenPublicationDateParsingFails_ShouldReturnFalseAndLog()
        {
            var result = Sut.SdnListBeenUpdated(new OfacFeedSubscription()
            {
                Channel = new Channel()
                {
                    PubDate = "not a date"
                }
            });

            Assert.False(result);
            _logger.Verify(x => x.Log(It.IsAny<string>(), LogLevel.Error));
        }

        [Fact]
        public void SdnListBeenUpdated_WhenDateTimeParsingSucceeds_ShouldCheckAgainstLatestUpdateDate()
        {
            var result = Sut.SdnListBeenUpdated(new OfacFeedSubscription()
            {
                Channel = new Channel()
                {
                    PubDate = "22 DEC 2020 10:10:10 EDT"
                }
            });

            _logger.Verify(x => x.Log(It.IsAny<string>(), It.IsAny<LogLevel>()), Times.Never);
            _sdnRepository.Verify(x => x.GetLastUpdateDateTime());
        }

        [Fact]
        public void SdnListBeenUpdated_WhenLastUpdateDateIsOlder_ShouldReturnTrue()
        {
            _sdnRepository.Setup(x => x.GetLastUpdateDateTime())
                          .Returns(() => new DateTime(2020, 12, 01, 10, 10, 10));

            var result = Sut.SdnListBeenUpdated(new OfacFeedSubscription()
            {
                Channel = new Channel()
                {
                    PubDate = "02 DEC 2020 10:10:10 EDT"
                }
            });

            _logger.Verify(x => x.Log(It.IsAny<string>(), It.IsAny<LogLevel>()), Times.Never);
            _sdnRepository.Verify(x => x.GetLastUpdateDateTime());

            Assert.True(result);
        }
    }
}