using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private const int PAGE_SIZE = 10;
        public SkillRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<PaginationResult<Skill>> GetAllAsync(string query, int page, CancellationToken cancellationToken)
        {
            // Filtro
            IQueryable<Skill> skills = _dbContext.Skills;

            if (!string.IsNullOrWhiteSpace(query))
            {
                skills = skills
                    .Where(p =>
                    p.Description.Contains(query) ||
                    EF.Functions.Like(p.Description, $"%{query}%"));
            }

            return await skills.GetPaged<Skill>(page, PAGE_SIZE);
        }

        public async Task AddSkillFromProject(Project project, CancellationToken cancellationToken)
        {
            // App Xamarin de Marketplace
            var words = project.Description.Split(' ');
            var length = words.Length;

            var skill = $"{project.Id} - {words[length - 1]}";
            // "1 - Marketplace"

            await _dbContext.Skills.AddAsync(new Skill(skill), cancellationToken);
        }
    }
}
