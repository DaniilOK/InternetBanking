namespace IB.Common.Helpers
{
    public static class StringHelper
    {
        public static string TrimToNull(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? null : str.Trim();
        }
    }
}
