using System.Security.Cryptography;
using System.Text;

namespace IB.Common.Cryptography
{
    /// <inheritdoc />
    /// <summary>
    /// Implements <see cref="T:CSI.Common.Cryptography.IHashAlgorithm" /> interface using MD5 hash.
    /// </summary>
    public sealed class Md5Hash : HashAlgorithm
    {
        protected override string CalculateHashInternal(string plainText)
        {
            var bytes = Encoding.Unicode.GetBytes(plainText);
            var hashed = MD5.Create().ComputeHash(bytes);
            return Encoding.Unicode.GetString(hashed);
        }
    }
}
