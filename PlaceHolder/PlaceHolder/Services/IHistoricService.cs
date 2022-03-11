namespace PlaceHolder.Services
{
    public interface IHistoricService
    {
        Ticket Create(Ticket user);

        Ticket Update(User user);

        void Delete(long id);

        Ticket FindyByID(long id);

        Ticket FindyByEmail(string email);

        List<Ticket> FindAll();
    }
}
