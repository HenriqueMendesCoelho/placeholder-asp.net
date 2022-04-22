using PlaceHolder.DTOs;

namespace PlaceHolder.Repositories.Implamentations
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        List<Ticket> FindAllByUserEmail(string email);
    }
}
