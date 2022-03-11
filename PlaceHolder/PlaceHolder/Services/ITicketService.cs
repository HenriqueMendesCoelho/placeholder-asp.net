using PlaceHolder.DTOs;

namespace PlaceHolder.Services
{
    public interface ITicketService
    {
        Ticket? CreateByUser(TicketCreateUserDTO ticket);

        Ticket? Update(Ticket ticket);

        Ticket? UpdateByEmployee(Ticket ticket);

        void Delete(long id);

        Ticket? FindByID(long id);

        List<Ticket> FindByUserEmail(string email);

        List<Ticket> FindAll();
    }
}
