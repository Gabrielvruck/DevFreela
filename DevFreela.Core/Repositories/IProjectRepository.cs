using DevFreela.Core.Entities;
using DevFreela.Core.Models;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<PaginationResult<Project>> GetAllAsync(string query, int page, CancellationToken cancellationToken);
        Task<Project> GetDetailsByIdAsync(int id, CancellationToken cancellationToken);
        Task StartAsync(Project project, CancellationToken cancellationToken);
        Task AddCommentAsync(ProjectComment projectComment, CancellationToken cancellationToken);
    }
}
