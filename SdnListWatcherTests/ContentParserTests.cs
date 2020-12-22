using System.Text.RegularExpressions;
using Common;
using Common.Interfaces;
using Moq;
using SdnListWatcher;
using Xunit;

namespace SdnListWatcherTests
{
    public class ContentParserTests
    {
        private const string Source = "Some random string";

        private readonly Mock<IRegexes> _regexes;
        private readonly ContentParser _contentParser;

        public ContentParserTests()
        {
            _regexes = new Mock<IRegexes>();

            //could use auto mocking
            _regexes.SetupGet(x => x.AddedItemRegex).Returns(new Regex("test"));
            _regexes.SetupGet(x => x.UpdatedItemRegex).Returns(new Regex("test"));
            _regexes.SetupGet(x => x.RemovedItemRegex).Returns(new Regex("test"));

            _contentParser = new ContentParser(_regexes.Object);
        }

        [Theory]
        [InlineData(SdnChangeType.Add)]
        [InlineData(SdnChangeType.Remove)]
        [InlineData(SdnChangeType.Update)]
        public void TryExtractSdnItems_WhenCalled_ShouldCallCorrectRegex(SdnChangeType sdnChangeType)
        {
            var result = _contentParser.TryExtractSdnItems(Source, sdnChangeType);

            switch (sdnChangeType)
            {
                case SdnChangeType.Add:
                    _regexes.VerifyGet(x => x.AddedItemRegex, Times.Once);
                    break;
                case SdnChangeType.Remove:
                    _regexes.VerifyGet(x => x.RemovedItemRegex, Times.Once);
                    break;
                case SdnChangeType.Update:
                    _regexes.VerifyGet(x => x.UpdatedItemRegex, Times.Once);
                    break;
            }
        }
    }
}