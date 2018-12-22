using System;
using System.Globalization;

namespace IB.Common.Helpers
{
    public static class DateTimeHelper
    {
        private static readonly CultureInfo Culture = new CultureInfo("en-US");

        public static long ToUnixEpochDate(this DateTime date)
        {
            return new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds();
        }
    }
}
