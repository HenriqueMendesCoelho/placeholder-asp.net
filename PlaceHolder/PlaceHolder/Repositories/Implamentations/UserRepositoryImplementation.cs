using PlaceHolder.Repositories.Generic;

namespace PlaceHolder.Repositories
{
    public class UserRepositoryImplementation : GenericRepository<User>, IUserRepository
    {
        private readonly DataContext _context;

        public UserRepositoryImplementation(DataContext context) : base (context) { }

        //Search user with the email
        public User? FindByEmailWithInclude(string email)
        {
            return _context.User.Include(u => u.Ticket).ThenInclude(t => t.Historical).AsNoTracking().SingleOrDefault(u => u.Email.Equals(email));
        }

        public User? FindByIdWithInclude(long id)
        {
            return _context.User.Include(u => u.Ticket).ThenInclude(t => t.Historical).AsNoTracking().SingleOrDefault(u => u.Id.Equals(id));
        }
    }
}
