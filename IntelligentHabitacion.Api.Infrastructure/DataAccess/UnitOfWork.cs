using IntelligentHabitacion.Api.Domain.Repository;
using System;

namespace IntelligentHabitacion.Api.Infrastructure.DataAccess
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IntelligentHabitacionContext _context;
        private bool _disposed;

        public UnitOfWork(IntelligentHabitacionContext context) => _context = context;

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _context.Dispose();

            _disposed = true;
        }
    }
}
