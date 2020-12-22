using System.Text.RegularExpressions;
using Common.Interfaces;
using Microsoft.Extensions.Options;
using Common.Configurations;

namespace SdnListWatcher
{
    public class Regexes : IRegexes
    {
        public Regex AddedItemRegex { get; }
        public Regex RemovedItemRegex { get; }
        public Regex UpdatedItemRegex { get; }
        public Regex LastNameRegex { get; }

        private RegexOptions RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Singleline;

        public Regexes(IOptions<RegexConfiguration> regexConfiguration)
        {
            var regexPatterns = regexConfiguration.Value;

            AddedItemRegex = new Regex(regexPatterns.Add, RegexOptions);
            RemovedItemRegex = new Regex(regexPatterns.Delete, RegexOptions);
            UpdatedItemRegex = new Regex(regexPatterns.Update, RegexOptions);
            LastNameRegex = new Regex(regexPatterns.LastName, RegexOptions);
        }
    }
}