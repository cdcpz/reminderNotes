using Ensolvers.Note.Domain.Contracts;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NoteContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(NoteContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> GenericRepository<T>() where T : class, IEntity
        {
            return new GenericRepository<T>(_context);
        }

        public async Task SaveChangesASync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }
    }
}
