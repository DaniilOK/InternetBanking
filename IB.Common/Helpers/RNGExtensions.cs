using System;
using System.Security.Cryptography;

namespace IB.Common.Helpers
{
    public static class RngExtensions
    {
        public static int NextInt(this RNGCryptoServiceProvider provider, int min, int max)
        {
            if (min > max) throw new ArgumentOutOfRangeException(nameof(min));
            if (min == max) return min;

            var data = new byte[4];
            provider.GetBytes(data);

            var generatedValue = Math.Abs(BitConverter.ToInt32(data, startIndex: 0));

            var diff = max - min;
            var mod = generatedValue % diff;
            var normalizedNumber = min + mod;

            return normalizedNumber;
        }

        public static long NextLong(this RNGCryptoServiceProvider provider, long min, long max)
        {
            if (min > max) throw new ArgumentOutOfRangeException(nameof(min));
            if (min == max) return min;

            var data = new byte[8];
            provider.GetBytes(data);

            var generatedValue = Math.Abs(BitConverter.ToInt64(data, startIndex: 0));

            var diff = max - min;
            var mod = generatedValue % diff;
            var normalizedNumber = min + mod;

            return normalizedNumber;
        }
    }
}
