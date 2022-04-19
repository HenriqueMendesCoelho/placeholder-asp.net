using PlaceHolder.DTOs;

namespace PlaceHolder.Repositories.Implamentations
{
    public interface IUserRepository 
    {
        User? FindByEmailWithInclude(string email);

        User? FindByIdWithInclude(long id);

    }
}
