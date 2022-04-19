using PlaceHolder.Repositories.Generic;

namespace PlaceHolder.Repositories.Implamentations
{
    public class TicketRepositoryImplementation : GenericRepository<Ticket>, ITicketRepository
    {

        private readonly DataContext _context;

        public TicketRepositoryImplementation(DataContext context) : base(context) { }

        public List<Ticket> FindAllByUserEmail(string email)
        {
            return _context.Ticket.Where(t => t.User.Email.Equals(email)).Include(t => t.Historical).AsNoTracking().ToList();
        }
    }
}
