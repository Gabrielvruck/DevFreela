using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        IProjectRepository Projects { get; }
        IUserRepository Users { get; }
        ISkillRepository Skills { get; }
        Task<int> CompleteAsync(CancellationToken cancellationToken);
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
