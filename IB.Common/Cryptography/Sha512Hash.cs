using System.Security.Cryptography;
using System.Text;

namespace IB.Common.Cryptography
{
    /// <inheritdoc />
    /// <summary>
    /// Implements <see cref="T:CSI.Common.Cryptography.IHashAlgorithm" /> interface using SHA512 hash.
    /// </summary>
    public sealed class Sha512Hash : HashAlgorithm
    {
        protected override string CalculateHashInternal(string plainText)
        {
            var bytes = Encoding.Unicode.GetBytes(plainText);
            var hashed = SHA512.Create().ComputeHash(bytes);
            return Encoding.Unicode.GetString(hashed);
        }
    }
}
