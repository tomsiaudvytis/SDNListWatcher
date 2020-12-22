using System.Text.RegularExpressions;

namespace Common.Interfaces
{
    public interface IRegexes
    {
        public Regex AddedItemRegex { get; }
        public Regex RemovedItemRegex { get; }
        public Regex UpdatedItemRegex { get; }
        public Regex LastNameRegex { get; }
    }
}