using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Domain.Contracts
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task AddAsync(T entity);
        Task AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> All();
        Task<T?> Find(string id);
        Task<T?> FirtsOfDefault(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> Where(Func<T, bool> predicate);
    }
}
