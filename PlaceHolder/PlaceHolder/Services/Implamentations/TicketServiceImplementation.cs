using PlaceHolder.DTOs;

namespace PlaceHolder.Services.Implamentations
{
    public class TicketServiceImplementation : ITicketService
    {
        private readonly ITicketRepository _repository;

        public TicketServiceImplementation(ITicketRepository repository)
        {
            _repository = repository;
        }

        public Ticket? CreateByUser(TicketCreateUserDTO ticket)
        {
            Ticket _ticket = convertTicketUserDTO(ticket);
            _ticket.Status = "Aberto";
            
            return _repository.Create(_ticket);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<Ticket> FindAll()
        {
            return _repository.FindAll();
        }

        public List<Ticket> FindByUserEmail(string email)
        {
            return _repository.FindAllByUserEmail(email);
        }

        public Ticket? FindByID(long id)
        {
            return _repository.FindByID(id);
        }

        public Ticket? Update(Ticket ticket)
        {
            return _repository.Update(ticket);
        }

        private Ticket convertTicketUserDTO(TicketCreateUserDTO obj)
        {
            return new Ticket()
            {
                Description = obj.Description,
                Category = obj.Category,
                SubCategory = obj.SubCategory,
                Title = obj.Title
            };
        }

        public Ticket? UpdateByEmployee(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
