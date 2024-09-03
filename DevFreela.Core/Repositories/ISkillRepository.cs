using DevFreela.Core.Dtos;

namespace DevFreela.Core.Repositories
{
    public interface ISkillRepository
    {
        Task<List<SkillDto>> GetAllAsync(CancellationToken cancellationToken);
    }
}
