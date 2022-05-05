using PlaceHolder.DTOs;

namespace PlaceHolder.Services.Implamentations
{
    public class TicketServiceImplementation : ITicketService
    {
        private readonly ITicketRepository _repository;
        private IUserService _userService;

        public TicketServiceImplementation(ITicketRepository repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public Ticket? CreateTicketByUser(TicketCreateByUserDTO obj, User user)
        {
            Ticket ticket = convertTicketUserDTO(obj, user);
            ticket.Status = Status.StatusEnum.ABERTO;
            ticket.CreationDate = DateTime.Now;

            _repository.Create(ticket);

            return ticket;
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
            return _repository.FindByIDWithInclude(id);
        }

        public Ticket? Update(Ticket ticket)
        {
            return _repository.Update(ticket);
        }

        public Ticket? UpdateByEmployee(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        private Ticket convertTicketUserDTO(TicketCreateByUserDTO obj, User user)
        {
            return new Ticket()
            {
                Description = obj.Description,
                Category = obj.Category,
                SubCategory = obj.SubCategory,
                Title = obj.Title,
                UserId = user.Id,
            };
        }
    }
}
