using PlaceHolder.DTOs;

namespace PlaceHolder.Repositories.Implamentations
{
    public interface ITicketRepository
    {
        Ticket? Create(Ticket ticket);

        Ticket? Update(Ticket ticket);

        void Delete(long id);

        Ticket? FindByID(long id);

        List<Ticket> FindAll();

        Boolean IsExist(Ticket ticket);

        List<Ticket> FindAllByUserEmail(string email);
    }
}
