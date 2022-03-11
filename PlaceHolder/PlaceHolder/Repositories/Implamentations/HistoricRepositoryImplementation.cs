namespace PlaceHolder.Repositories.Implamentations
{
    public class HistoricRepositoryImplementation : IHistoricRepository
    {

        private readonly DataContext _context;

        public HistoricRepositoryImplementation(DataContext context)
        {
            _context = context;
        }

        public Historic? Create(Historic historic)
        {
            if(historic == null) return null;
            try
            {
                _context.Historic.Add(historic);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return historic;
        }

        public void Delete(long id)
        {
            var historic = _context.Historic.SingleOrDefault(h => h.Id.Equals(id));

            if (historic != null)
            {
                try
                {
                    _context.Historic.Remove(historic);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<Historic> FindAll()
        {
            return _context.Historic.ToList();
        }

        public Historic? FindByID(long id)
        {
            return _context.Historic.SingleOrDefault(h => h.Id.Equals(id));
        }

        public bool IsExist(Historic historic)
        {
            return _context.Historic.Any(h => h.Id.Equals(historic.Id));
        }

        public Historic? Update(Historic historic)
        {
            if (!IsExist(historic)) return null;

            var _historic = _context.Historic.SingleOrDefault(t => t.Id.Equals(historic.Id));

            if (_historic != null)
            {
                try
                {
                    _context.Entry(_historic).CurrentValues.SetValues(historic);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return historic;
        }
    }
}
