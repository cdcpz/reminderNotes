using Ensolvers.Note.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ensolvers.Note.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly NoteContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(NoteContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<T>> All()
        {
            return await _dbSet.ToListAsync();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<T?> Find(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T?> FirtsOfDefault(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate, default);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<IEnumerable<T>> Where(Func<T, bool> predicate)
        {
            return await Task.FromResult(_dbSet.Where(predicate));
        }
    }
}