namespace PlaceHolder.Services.Implamentations
{
    public class HistoricServiceImplementation : IHistoricService
    {

        private readonly IRepository<Historic> _repository;

        public HistoricServiceImplementation(IRepository<Historic> repository)
        {
            _repository = repository;
        }

        public Historic Create(Historic historic)
        {
            _repository.Create(historic);

            return historic;
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<Historic> FindAll()
        {
            return _repository.FindAll();
        }

        public Historic? FindByID(long id)
        {
            return _repository.FindByID(id);
        }

        public Historic? Update(Historic Historic)
        {
            return _repository.Update(Historic);
        }
    }
}
