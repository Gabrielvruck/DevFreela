using DevFreela.Core.Entities;
using DevFreela.Core.Models;

namespace DevFreela.Core.Repositories
{
    public interface ISkillRepository
    {
        Task<PaginationResult<Skill>> GetAllAsync(string query, int page, CancellationToken cancellationToken);
        Task AddSkillFromProject(Project project, CancellationToken cancellationToken);
    }
}
