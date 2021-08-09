using System;

namespace OpenSky
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static int ToUnixTimestamp(this DateTime time)
        {
            var result = (int)time.ToUniversalTime().Subtract(epoch).TotalSeconds;
            if (result < 0)
            {
                return -1;
            }

            return result;
        }

        public static DateTime FromUnixTimestamp(this int unixTimestamp)
            => epoch.AddSeconds(unixTimestamp);

        public static DateTime FromUnixTimestamp(this long unixTimestamp)
            => epoch.AddMilliseconds(unixTimestamp);

        public static DateTime? FromUnixTimestamp(this int? unixTimestamp)
        {
            if (unixTimestamp == null)
            {
                return null;
            }

            return unixTimestamp.Value.FromUnixTimestamp();
        }
    }
}
