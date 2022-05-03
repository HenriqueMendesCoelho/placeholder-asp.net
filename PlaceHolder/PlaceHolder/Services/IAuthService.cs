using PlaceHolder.DTOs;

namespace PlaceHolder.Services
{
    public interface IAuthService
    {
        TokenDTO? Login(UserLoginDTO obj);

        TokenDTO? RefreshToken(RefreshTokenDTO obj);
    }
}
