using PlaceHolder.DTOs;
using PlaceHolder.Exceptions;
using PlaceHolder.Integrations.ViaCEP;
using PlaceHolder.Integrations.ViaCEP.Model;

namespace PlaceHolder.Services.Implamentations
{
    public class TicketServiceImplementation : ITicketService
    {
        private readonly ITicketRepository _repository;
        private readonly ITicketAddressService _addressService;
        private IUserService _userService;
        private readonly ILogger<TicketServiceImplementation> _logger;

        public TicketServiceImplementation(ITicketRepository repository, ITicketAddressService addressService, IUserService userService, ILogger<TicketServiceImplementation> logger)
        {
            _repository = repository;
            _addressService = addressService;
            _userService = userService;
            _logger = logger;
        }

        public Ticket? CreateTicketByUser(TicketCreateByUserDTO obj, User user)
        {

            //List<string> category = new List<string> { "Teste22", "Teste2" };

            //if (category.Contains("Teste")) throw new ApiInternalException("Categoria não encontrada");

            Ticket ticket = ConvertTicketUserDTO(obj, user.Id);
            ticket.Status = Status.StatusEnum.ABERTO;
            ticket.CreationDate = DateTime.Now;
            
            ViaCEPResponse viaCEPResponse;
            ViaCEPIntegration viaCEP = new();

            viaCEPResponse = viaCEP.ValidateCEPWrap(Convert.ToString(obj.Cep));

            Ticket createdTicket = _repository.Create(ticket);

            if(createdTicket != null) _addressService.Create(ExtractAddresFromTicketCreateByUserDTO(obj, createdTicket.Id, viaCEPResponse));
            return ticket;
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<Ticket> FindAll()
        {
            return _repository.FindAllWithInclude();
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

        private Ticket ConvertTicketUserDTO(TicketCreateByUserDTO obj, long id)
        {
            return new Ticket()
            {
                Description = obj.Description,
                Category = obj.Category,
                SubCategory = obj.SubCategory,
                Title = obj.Title,
                UserId = id,
            };
        }

        private TicketAddress ExtractAddresFromTicketCreateByUserDTO(TicketCreateByUserDTO obj, long id, ViaCEPResponse viaCEP)
        {
            return new TicketAddress()
            {
                Cep = viaCEP.Cep.Replace("-", ""),
                State = (!string.IsNullOrEmpty(viaCEP.Uf)) ? viaCEP.Uf : obj.State,
                City = (!string.IsNullOrEmpty(viaCEP.Localidade)) ? viaCEP.Localidade : obj.City,
                District = (!string.IsNullOrEmpty(viaCEP.Bairro)) ? viaCEP.Bairro : obj.District,
                Street = (!string.IsNullOrEmpty(viaCEP.Logradouro)) ? viaCEP.Logradouro : obj.Street,
                Complement = obj.Complement,
                Id = id,
                Number = obj.Number
            };
        }

        public List<Ticket> FindAllWithIncludes()
        {
            return _repository.FindAllWithInclude();
        }
    }
}
