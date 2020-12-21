namespace SdnListWatcher.Extensions
{
    public static class StringExtensions
    {
        public static string CleanSdnName(this string name)
        {
            return name.Replace(@"<br />", string.Empty)
                       .Replace(@",", string.Empty)
                       .Replace(@"(", string.Empty)
                       .Trim();
        }
    }
}