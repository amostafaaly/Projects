using Microsoft.EntityFrameworkCore;
using Projects.Src.Data;
using Projects.Src.Interfaces;
using Projects.Src.Shared;

namespace Projects.Src.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public EfRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException("Add", ex.Message);
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return _dbSet.ToList();
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException("GetAll", ex.Message);
            }
        }

        public T? GetById(string id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException("GetById", ex.Message);
            }
        }

        public void Remove(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException("Remove", ex.Message);
            }
        }

        public void Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException("Update", ex.Message);
            }
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            try
            {
                return _dbSet.Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException("Find", ex.Message);
            }
        }
    }
}
