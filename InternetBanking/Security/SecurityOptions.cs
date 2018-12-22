namespace InternetBanking.Security
{
    public class SecurityOptions
    {
        public string SecretKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public int Lifetime { get; set; }
    }
}
