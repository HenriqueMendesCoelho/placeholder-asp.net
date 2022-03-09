namespace Backlog.Repositories.Implamentations
{
    public class TicketRepositoryImplementation : ITicketRepository
    {

        private readonly DataContext _context;

        public TicketRepositoryImplementation(DataContext context)
        {
            _context = context;
        }

        public Ticket? Create(Ticket ticket)
        {
            if(ticket == null) return null;
            try
            {
                _context.Ticket.Add(ticket);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return ticket;
        }

        public void Delete(long id)
        {
            var ticket = _context.Ticket.SingleOrDefault(t => t.Id.Equals(id));

            if (ticket != null)
            {
                try
                {
                    _context.Ticket.Remove(ticket);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<Ticket> FindAll()
        {
            return _context.Ticket.ToList();
        }

        public Ticket? FindByID(long id)
        {
            return _context.Ticket.Include(t => t.Historics).AsNoTracking().SingleOrDefault(t => t.Id.Equals(id));
        }

        public bool IsExist(Ticket ticket)
        {
            return _context.Ticket.Any(t => t.Id.Equals(ticket.Id));
        }

        public Ticket? Update(Ticket ticket)
        {
            if (!IsExist(ticket)) return null;

            var _ticket = _context.Ticket.SingleOrDefault(t => t.Id.Equals(ticket.Id));

            if (_ticket != null)
            {
                try
                {
                    _context.Entry(_ticket).CurrentValues.SetValues(ticket);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return ticket;
        }
    }
}
