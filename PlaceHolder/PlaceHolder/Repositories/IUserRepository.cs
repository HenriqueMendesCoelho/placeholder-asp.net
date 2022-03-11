using PlaceHolder.DTOs;

namespace PlaceHolder.Repositories.Implamentations
{
    public interface IUserRepository
    {
        User? Create(User user);

        User? Update(User user);

        void Delete(long id);

        User? FindByID(long id);

        User? FindByEmail(string email);

        List<User> FindAll();

        Boolean IsExist(User user);
    }
}
