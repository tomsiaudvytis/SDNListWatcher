using System;
using System.Text.RegularExpressions;
using Common;
using Common.Interfaces;
using Microsoft.Extensions.Options;

namespace SdnListWatcher
{
    public class ContentParser : IContentParser
    {
        private RegexOptions RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Singleline;

        private readonly Regex _addedItemRegex;
        private readonly Regex _removedItemRegex;
        private readonly Regex _updatedItemRegex;
        private readonly Regex _lastNameRegex;

        public ContentParser(IOptions<Common.Configurations.RegexConfigurationPattern> regexConfigurationPattern)
        {
            var regexPatterns = regexConfigurationPattern.Value;

            _addedItemRegex = new Regex(regexPatterns.Add, RegexOptions);
            _removedItemRegex = new Regex(regexPatterns.Delete, RegexOptions);
            _updatedItemRegex = new Regex(regexPatterns.Update, RegexOptions);
            _lastNameRegex = new Regex(regexPatterns.LastName, RegexOptions);
        }

        public MatchCollection TryExtractSdnItems(string source, SdnChangeType sdnChangeType)
        {
            return sdnChangeType switch
            {
                SdnChangeType.Add => _addedItemRegex.Matches(source),
                SdnChangeType.Remove => _removedItemRegex.Matches(source),
                SdnChangeType.Update => _updatedItemRegex.Matches(source),
                _ => throw new ArgumentOutOfRangeException(nameof(sdnChangeType), sdnChangeType,
                    "SdnChangeType Type not implemented")
            };
        }

        public MatchCollection TryExtractLastName(string source) => _lastNameRegex.Matches(source);
    }
}