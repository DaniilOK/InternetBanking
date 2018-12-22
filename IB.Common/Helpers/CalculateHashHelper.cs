using IB.Common.Cryptography;

namespace IB.Common.Helpers
{
    public static class CalculateHashHelper
    {
        public static string ComputeHash(string str)
        {
            Expect.ArgumentNotNull(str, nameof(str));

            IHashAlgorithm hashAlgorithm = new Md5Hash();
            return hashAlgorithm.CalculateHash(str);
        }
    }
}
