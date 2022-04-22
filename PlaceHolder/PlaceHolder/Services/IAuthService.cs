using PlaceHolder.DTOs;

namespace PlaceHolder.Services
{
    public interface IAuthService
    {
        TokenDTO? ValidateCredencials(UserLoginDTO user);
    }
}
