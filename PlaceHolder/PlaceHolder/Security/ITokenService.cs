using System.Security.Claims;

namespace PlaceHolder.Security
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipal(string token);
    }
}
