using PlaceHolder.DTOs;
using System.Security.Cryptography;

namespace PlaceHolder.Repositories.Implamentations
{
    public interface IUserRepository : IRepository<User>
    {
        User? FindByEmailWithInclude(string email);

        User? FindByEmail(string email);

        User? FindByIDWithInclude(long id);

        User? FindByCPF(string cpf);

        User? ValidateCredencials(UserLoginDTO user);

        String EncryptPassword(string input, SHA512 algorithm);

    }
}
