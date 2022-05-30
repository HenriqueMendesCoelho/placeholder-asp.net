using PlaceHolder.DTOs;
using PlaceHolder.Repositories.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PlaceHolder.Repositories
{
    public class UserRepositoryImplementation : GenericRepository<User>, IUserRepository
    {

        public UserRepositoryImplementation(DataContext Context) : base(Context) { }

        public User? FindByCPF(string cpf)
        {
            return _context.User.SingleOrDefault(u => u.Cpf.Equals(cpf));
        }

        public User? FindByEmail(string email)
        {
            return _context.User.SingleOrDefault(u => u.Email.Equals(email));
        }

        //Search user with the email
        public User? FindByEmailWithInclude(string email)
        {
            return _context.User
                .Include(u => u.Ticket).ThenInclude(t => t.Historical)
                .Include(u => u.Ticket).ThenInclude(t => t.Address)
                .Include(u => u.Address)
                .AsNoTracking().SingleOrDefault(u => u.Email.Equals(email));
        }

        public User? FindByIDWithInclude(long id)
        {
            return _context.User
                .Include(u => u.Ticket).ThenInclude(t => t.Historical)
                .Include(u => u.Ticket).ThenInclude(t => t.Address)
                .Include(u => u.Address)
                .AsNoTracking().SingleOrDefault(u => u.Id.Equals(id));
        }

        public User? ValidateCredencials(UserLoginDTO obj)
        {
            var pass = EncryptPassword(obj.Password, SHA512.Create());

            User user = _context.User.FirstOrDefault(u => u.Email.Equals(obj.Email) && u.Password.Equals(pass));

            return user;
        }

        public string EncryptPassword(string input, SHA512 algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
    }
}
