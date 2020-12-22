using System;
using System.Text.RegularExpressions;
using Common;
using Common.Interfaces;
using Microsoft.Extensions.Options;

namespace SdnListWatcher
{
    public class ContentParser : IContentParser
    {
        private readonly IRegexes _regexes;
        public ContentParser(IRegexes regexes) => _regexes = regexes;

        public MatchCollection TryExtractSdnItems(string source, SdnChangeType sdnChangeType)
        {
            return sdnChangeType switch
            {
                SdnChangeType.Add => _regexes.AddedItemRegex.Matches(source),
                SdnChangeType.Remove => _regexes.RemovedItemRegex.Matches(source),
                SdnChangeType.Update => _regexes.UpdatedItemRegex.Matches(source),
                _ => throw new ArgumentOutOfRangeException(nameof(sdnChangeType), sdnChangeType,
                    "SdnChangeType Type not implemented")
            };
        }

        public MatchCollection TryExtractLastName(string source) => _regexes.LastNameRegex.Matches(source);
    }
}