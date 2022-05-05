namespace PlaceHolder.Services
{
    public interface IHistoricService
    {
        Historic Create(Historic historic);

        Historic? Update(Historic Historic);

        void Delete(long id);

        Historic? FindByID(long id);

        List<Historic> FindAll();
    }
}
