using Backlog.DTOs;

namespace Backlog.Repositories.Implamentations
{
    public interface ITicketRepository
    {
        Ticket? Create(Ticket ticket);

        Ticket? Update(Ticket ticket);

        void Delete(long id);

        Ticket? FindByID(long id);

        List<Ticket> FindAll();

        Boolean IsExist(Ticket ticket);
    }
}
