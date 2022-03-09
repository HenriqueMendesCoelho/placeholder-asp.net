using Backlog.Repositories.Implamentations;

namespace Backlog.Repositories
{
    public class UserRepositoryImplementation : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepositoryImplementation(DataContext context)
        {
            _context = context;
        }
        //Create user in the DB
        public User? Create(User user)
        {
            if(user == null) return null;
            try
            {
                _context.User.Add(user);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }           
            return user;
        }
        //Delete user from the DB
        public void Delete(long id)
        {
            var user = _context.User.SingleOrDefault(u => u.Id.Equals(id));

            if (user != null)
            {
                try
                {
                    _context.User.Remove(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        //List all Users
        public List<User> FindAll()
        {
            return _context.User.ToList();
        }
        //Search user with the email
        public User? FindByEmail(string email)
        {
            return _context.User.Include(u => u.Ticket).ThenInclude(t => t.Historics).AsNoTracking().SingleOrDefault(u => u.Email.Equals(email));
        }
        //Search user with de id
        public User? FindByID(long id)
        {
            return _context.User.Include(u => u.Ticket).ThenInclude(t => t.Historics).AsNoTracking().SingleOrDefault(u => u.Id.Equals(id));
        }
        //Validation if a user exist
        public bool IsExist(User user)
        {
            return _context.User.Any(u => u.Email.Equals(user.Id));
        }
        //Update the user
        public User? Update(User user)
        {
            if(!IsExist(user)) return null;

            var _user = _context.User.SingleOrDefault(u => u.Id.Equals(user.Id));

            if(_user != null)
            {
                try
                {
                    _context.Entry(_user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return user;
        }
    }
}
