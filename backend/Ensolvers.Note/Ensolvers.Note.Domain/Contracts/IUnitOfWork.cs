using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GenericRepository<T>() where T : class, IEntity;
        Task SaveChangesASync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
