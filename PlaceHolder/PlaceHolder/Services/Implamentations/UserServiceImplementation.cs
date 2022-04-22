using PlaceHolder.DTOs;
using PlaceHolder.Exceptions;
using System.Security.Cryptography;

namespace PlaceHolder.Services
{
    public class UserServiceImplementation : IUserService
    {
        private readonly IUserRepository _repository;

        public UserServiceImplementation(IUserRepository userRepository)
        {
            _repository = userRepository;
        }

        public User Create(UserDTO obj)
        {
            
            //Converting DTO to a User
            User user = ConvertToUser(obj);
            if(user != null)
            {
                user.Role = "user";

                if (FindAll == null) user.Role = "admin";

                user.Password = _repository.EncryptPassword(obj.Password, SHA512.Create());
                user.RefreshTokenExpiryTime = DateTime.Now;

                try
                {
                    _repository.Create(user);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw new ApiInternalException("Internal Server Error - Contact Administrator");
                }
                
            }

#pragma warning disable CS8603 // Possible null reference return.
            return user;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Delete(string email)
        {
            if(email == null)
                throw new Exception(string.Format("E-mail can not be null or empty"));

            var user = _repository.FindByEmailWithInclude(email);

            if (user == null)
                throw new Exception(string.Format("User can not be found"));

            _repository.Delete(user.Id);
        }

        public List<User> FindAll()
        {
            return _repository.FindAll();
        }

        public User? FindByEmail(string email)
        {
            return _repository.FindByEmailWithInclude(email);
        }

        public User? FindByID(long id)
        {
            return _repository.FindByIDWithInclude(id);
        }
        public User? Update(User user)
        {
            return _repository.Update(user);
        }

        //Convert CreateUserDTO in a User class
        private User ConvertToUser(UserDTO obj)
        {
            User u = new User
            {
                FullName = obj.FullName,
                Email = obj.Email,
                Password = obj.Password,
                Cpf = obj.Cpf,
                BackupEmail = obj.BackupEmail
            };
            return u;
        }

    
    }
}
