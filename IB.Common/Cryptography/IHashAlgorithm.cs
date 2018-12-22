namespace IB.Common.Cryptography
{
    /// <summary>
    /// Provides an interface for computing the hash string.
    /// </summary>
    public interface IHashAlgorithm
    {
        /// <exception cref="System.ArgumentNullException"><paramref name="plainText" /> is null.</exception>
        /// <exception cref="System.ArgumentException">Length of <paramref name="plainText" /> nameof(plainText).</exception>
        string CalculateHash(string plainText);
    }
}
