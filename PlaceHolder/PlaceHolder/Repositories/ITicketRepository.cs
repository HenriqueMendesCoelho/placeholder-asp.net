using PlaceHolder.DTOs;

namespace PlaceHolder.Repositories.Implamentations
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        List<Ticket> FindAllByUserEmail(string email);

        List<Ticket> FindAllWithInclude();

        Ticket? FindByIDWithInclude(long id);
    }
}
