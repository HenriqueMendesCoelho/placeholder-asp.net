using PlaceHolder.DTOs;
using PlaceHolder.Exceptions;
using System.Security.Cryptography;

namespace PlaceHolder.Services
{
    public class UserServiceImplementation : IUserService
    {
        private readonly ILogger<UserServiceImplementation> _logger;
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
                user.profile = Profiles.ProfilesEnum.USER;
                user.CreationDate = DateTime.Now;

                if (FindAll().Count() <= 0) user.profile = Profiles.ProfilesEnum.ADMIN;

                user.Password = _repository.EncryptPassword(obj.Password, SHA512.Create());
                user.RefreshTokenExpiryTime = DateTime.Now;

                try
                {
                    _repository.Create(user);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    throw;
                }
                
            }

#pragma warning disable CS8603 // Possible null reference return.
            return user;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Delete(string email)
        {
            User user = _repository.FindByEmailWithInclude(email);

            if (user == null) throw new ApiInternalException("User not found");

            _repository.Delete(user.Id);
        }

        public List<User> FindAll()
        {
            List<User> users = _repository.FindAll();

            foreach (User user in users)
            {
                user.Password = "*****";
            }

            return users;
        }

        public User? FindByEmail(string email)
        {
            var user = _repository.FindByEmailWithInclude(email);
            if (user == null) return null;

            return user;
        }

        public User? FindByID(long id)
        {
            var user = _repository.FindByIDWithInclude(id);
            if(user == null) return null;

            return user;
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
