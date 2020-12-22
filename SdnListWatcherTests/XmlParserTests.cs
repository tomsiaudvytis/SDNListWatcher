using Common.Enums;
using Common.Interfaces;
using Moq;
using SdnListWatcher;
using Xunit;

namespace SdnListWatcherTests
{
    public class XmlParserTests
    {
        [Fact]
        public void TryDeserializeObject_WhenFails_ShouldReturnFalseAndLog()
        {
            var logger = new Mock<ILogger>();
            var xmlParser = new XmlParser(logger.Object);

            var content = "Some very invalid xml";
            var isSuccess = xmlParser.TryDeserializeObject(out string result, content);

            Assert.False(isSuccess);
            logger.Verify(x => x.Log(It.IsAny<string>(), LogLevel.Fatal), Times.Once);
        }
    }
}