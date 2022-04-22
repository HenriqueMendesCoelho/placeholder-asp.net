namespace PlaceHolder.DTOs
{
    public class TokenDTO
    {
        public bool Authenticated { get; set; }

        public DateTime Created { get; set; }

        public DateTime Expiration { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public TokenDTO(bool authenticated, DateTime created, DateTime expiration, string accessToken, string refreshToken)
        {
            Authenticated = authenticated;
            Created = created;
            Expiration = expiration;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
