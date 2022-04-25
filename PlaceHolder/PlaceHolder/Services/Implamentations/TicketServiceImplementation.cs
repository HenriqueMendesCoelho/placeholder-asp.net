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

        public Ticket? CreateByUser(TicketCreateUserDTO obj)
        {
            Ticket ticket = convertTicketUserDTO(obj);
            ticket.Status = Status.StatusEnum.ABERTO;
            ticket.CreationDate = DateTime.Now;
            
            return _repository.Create(ticket);
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
