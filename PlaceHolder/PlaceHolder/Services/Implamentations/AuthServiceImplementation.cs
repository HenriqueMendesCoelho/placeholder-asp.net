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

        public TokenDTO? ValidateCredencials(UserLoginDTO obj)
        {
            var user = _userRepository.ValidateCredencials(obj);

            if(user == null) return null;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                //new Claim(ClaimTypes.Role, user.Role) -- Insert Role in token
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpire);

            _userRepository.Update(user);

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
