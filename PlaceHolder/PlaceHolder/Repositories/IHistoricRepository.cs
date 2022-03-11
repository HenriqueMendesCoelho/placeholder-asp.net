namespace PlaceHolder.Repositories.Implamentations
{
    public interface IHistoricRepository
    {
        Historic? Create(Historic historic);

        Historic? Update(Historic historic);

        void Delete(long id);

        Historic? FindByID(long id);

        List<Historic> FindAll();

        Boolean IsExist(Historic historic);
    }
}
