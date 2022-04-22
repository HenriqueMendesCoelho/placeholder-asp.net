namespace PlaceHolder.Configurations
{
    public class TokenConfiguration
    {
        public const string ConfigurationProps = "TokenConfiguratios";

        public string Audience { get; set; }

        public string Issuer { get; set; }

        public string Secret { get; set; }

        public int Minutes { get; set; }

        public int DaysToExpire { get; set; }

        public TokenConfiguration() { }

        public TokenConfiguration(string audience, string issuer, string secret, int minutes, int daysToExpire)
        {
            Audience = audience;
            Issuer = issuer;
            Secret = secret;
            Minutes = minutes;
            DaysToExpire = daysToExpire;
        }
    }
}
