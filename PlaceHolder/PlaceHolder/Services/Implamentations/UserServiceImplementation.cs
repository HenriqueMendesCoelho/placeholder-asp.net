using PlaceHolder.DTOs;

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

            _repository.Create(user);
            return user;
        }

        public void Delete(string email)
        {
            if(email == null)
                throw new Exception(string.Format("E-mail can not be null or empty"));

            var user = _repository.FindByEmail(email);

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
            return _repository.FindByEmail(email);
        }

        public User? FindByID(long id)
        {
            return _repository.FindByID(id);
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
