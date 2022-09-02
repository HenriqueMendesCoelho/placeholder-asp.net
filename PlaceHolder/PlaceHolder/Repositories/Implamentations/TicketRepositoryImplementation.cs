using PlaceHolder.Repositories.Generic;

namespace PlaceHolder.Repositories.Implamentations
{
    public class TicketRepositoryImplementation : GenericRepository<Ticket>, ITicketRepository
    {

        public TicketRepositoryImplementation(DataContext context) : base(context) { }

        public List<Ticket> FindAllByUserEmail(string email)
        {
            return _context.Ticket
                .Where(t => t.User.Email.Equals(email))
                .Include(t => t.Historical)
                .Include(t => t.Address)
                .AsNoTracking().ToList();
        }

        public List<Ticket> FindAllWithInclude()
        {
            return _context.Ticket
                .Include(t => t.Address)
                .Include(t => t.Historical)
                .ToList();
        }

        public Ticket? FindByIDWithInclude(long id)
        {
            return _context.Ticket
                .Include(t => t.Historical)
                .Include(t => t.Address)
                .AsNoTracking().SingleOrDefault(t => t.Id.Equals(id));
        }
    }
}
