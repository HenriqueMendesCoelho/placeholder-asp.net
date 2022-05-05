using PlaceHolder.Configurations;
using PlaceHolder.DTOs;
using PlaceHolder.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PlaceHolder.Services.Implamentations
{
    public class AuthServiceImplementation : IAuthService
    {
        private TokenConfiguration _configuration;

        private IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthServiceImplementation(TokenConfiguration configuration, IUserRepository userRepository, ITokenService tokenService)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public TokenDTO? Login(UserLoginDTO obj)
        {
            var user = _userRepository.ValidateCredencials(obj);

            if(user == null) return null;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                //new Claim(ClaimTypes.Role, user.profile.ToString()) //-- Insert Role in token
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpire);

            _userRepository.Update(user);

            return GenerateTokenDTO(accessToken, refreshToken);
        }

        public TokenDTO? RefreshToken(RefreshTokenDTO obj)
        {
            ClaimsPrincipal principal = _tokenService.GetPrincipal(obj.AccessToken.ToString().Substring(7));
            User user = _userRepository.FindByEmail(principal.Identity.Name);

            if(user == null || 
                user.RefreshToken != obj.RefreshToken || 
                user.RefreshTokenExpiryTime <= DateTime.Now) return null;

            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpire);
            user.RefreshToken = _tokenService.GenerateRefreshToken();

            _userRepository.Update(user);

            return GenerateTokenDTO(_tokenService.GenerateAccessToken(principal.Claims), user.RefreshToken);
        }

        private TokenDTO? GenerateTokenDTO(string accessToken, string refreshToken)
        {
            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenDTO(
                true,
                createDate,
                expirationDate,
                accessToken,
                refreshToken
            );
        }
    }
}
