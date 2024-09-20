using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevFreela.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        private readonly DevFreelaDbContext _context;
        public UnitOfWork(
            DevFreelaDbContext context,
            IProjectRepository projects,
            IUserRepository users,
            ISkillRepository skills
            )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Projects = projects ?? throw new ArgumentNullException(nameof(projects));
            Users = users ?? throw new ArgumentNullException(nameof(users));
            Skills = skills ?? throw new ArgumentNullException(nameof(skills));
        }

        public IProjectRepository Projects { get; }
        public IUserRepository Users { get; }
        public ISkillRepository Skills { get; }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync(cancellationToken);
                throw ex;
            }
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
