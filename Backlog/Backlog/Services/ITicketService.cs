namespace Backlog.Services
namespace Backlog.Services
{
    public interface ITicketService
    {
        Ticket Create(Ticket user);

        Ticket Update(User user);

        void Delete(long id);

        Ticket FindyByID(long id);

        Ticket FindyByEmail(string email);

        List<Ticket> FindAll();
    }
}
