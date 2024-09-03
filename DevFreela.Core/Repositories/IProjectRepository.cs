using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync(CancellationToken cancellationToken);
        Task<Project> GetDetailsByIdAsync(int id, CancellationToken cancellationToken);
        Task<Project> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(Project project, CancellationToken cancellationToken);
        Task StartAsync(Project project, CancellationToken cancellationToken);
        Task AddCommentAsync(ProjectComment projectComment, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
