using System;
using System.Globalization;

namespace SdnListWatcher.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime? ParsePublicationDate(this string date)
        {
            return DateTime.TryParseExact(date, "dd MMM yyyy HH:mm:ss EDT", null, DateTimeStyles.None, out var result)
                ? result
                : (DateTime?) null;
        }
    }
}