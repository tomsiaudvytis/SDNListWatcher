using System.Text.RegularExpressions;

namespace Common.Interfaces
{
    public interface IContentParser
    {
        MatchCollection TryExtractSdnItems(string source, SdnChangeType sdnChangeType);
        MatchCollection TryExtractLastName(string source);
    }
}