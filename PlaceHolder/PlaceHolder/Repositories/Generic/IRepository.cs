using PlaceHolder.Models.Base;

namespace PlaceHolder.Repositories.Implamentations
{
    public interface IRepository<T> where T : BaseEntity
    {
        T? Create(T item);

        T? Update(T item);

        void Delete(long id);

        T? FindByID(long id);

        List<T> FindAll();

        Boolean Exists(T item);
    }
}
