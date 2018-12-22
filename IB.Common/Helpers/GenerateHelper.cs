using System.Security.Cryptography;

namespace IB.Common.Helpers
{
    public static class GenerateHelper
    {
        private static readonly RNGCryptoServiceProvider Provider = new RNGCryptoServiceProvider();

        public static int GeneratePinCode()
        {
            return Provider.NextInt(1000, 9999);
        }

        public static int GenerateVerificationCode()
        {
            return Provider.NextInt(100, 999);
        }

        public static long GenerateAccountNumber()
        {
            return Provider.NextLong(100000000000000000, 999999999999999999);
        }

        public static long GenerateCardNumber()
        {
            return Provider.NextLong(1000000000000000, 9999999999999999);
        }
    }
}
