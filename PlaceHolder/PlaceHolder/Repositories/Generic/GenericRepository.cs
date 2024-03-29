﻿using PlaceHolder.Models.Base;

namespace PlaceHolder.Repositories.Generic
{

    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        protected readonly DataContext _context;

        public DbSet<T> dataset { get; set; }

        public GenericRepository(DataContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }
        public T? Create(T item)
        {
            if (item == null) return null;
            try
            {
                dataset.Add(item);
                _context.SaveChanges();

                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(r => r.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T? FindByID(long id)
        {
            return dataset.SingleOrDefault(r => r.Id.Equals(id));
        }

        public bool Exists(T item)
        {
            return dataset.Any(i => i.Id.Equals(item.Id));
        }

        public T? Update(T item)
        {
            var result = dataset.SingleOrDefault(r => r.Id.Equals(item.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return null;
        }
    }
}
