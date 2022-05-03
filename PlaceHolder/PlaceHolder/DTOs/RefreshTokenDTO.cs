namespace PlaceHolder.DTOs
{
    public class RefreshTokenDTO
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public RefreshTokenDTO() {  }

        public RefreshTokenDTO(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
