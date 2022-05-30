namespace PlaceHolder.Services.Implamentations
{
    public class TicketAddressImplementation : ITicketAddressService
    {

        private readonly IRepository<TicketAddress> _repository;

        public TicketAddressImplementation(IRepository<TicketAddress> repository)
        {
            _repository = repository;
        }

        public TicketAddress Create(TicketAddress ticketAddress)
        {
            _repository.Create(ticketAddress);

            return ticketAddress;
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<TicketAddress> FindAll()
        {
            return _repository.FindAll();
        }

        public TicketAddress? FindByID(long id)
        {
            return _repository.FindByID(id);
        }

        public TicketAddress? Update(TicketAddress ticketAddress)
        {
            return _repository.Update(ticketAddress);
        }
    }
}
