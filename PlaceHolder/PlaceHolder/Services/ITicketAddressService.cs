namespace PlaceHolder.Services
{
    public interface ITicketAddressService
    {
        TicketAddress Create(TicketAddress ticketAddress);

        TicketAddress? Update(TicketAddress ticketAddress);

        void Delete(long id);

        TicketAddress? FindByID(long id);

        List<TicketAddress> FindAll();
    }
}
