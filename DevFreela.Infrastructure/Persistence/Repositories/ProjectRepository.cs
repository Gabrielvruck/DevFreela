using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        private readonly string _connectionString;
        private const int PAGE_SIZE = 10;
        public ProjectRepository(DevFreelaDbContext context, IConfiguration configuration) : base(context)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task AddCommentAsync(ProjectComment projectComment, CancellationToken cancellationToken)
        {
            await _dbContext.ProjectComments.AddAsync(projectComment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<PaginationResult<Project>> GetAllAsync(string query, int page, CancellationToken cancellationToken)
        {
            // Filtro
            IQueryable<Project> projects = _dbContext.Projects;

            if (!string.IsNullOrWhiteSpace(query))
            {
                projects = projects
                    .Where(p =>
                        p.Title.Contains(query) ||
                        EF.Functions.Like(p.Description, $"%{query}%"));
            }

            return await projects.GetPaged<Project>(page, PAGE_SIZE);
        }

        public async Task<Project> GetDetailsByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Projects
               .Include(p => p.Client)
               .Include(p => p.Freelancer)
               .SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        }

        public async Task StartAsync(Project project, CancellationToken cancellationToken)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync(cancellationToken);

                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedat WHERE Id = @id";

                await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedat = project.StartedAt, project.Id });
            }
        }
    }
}
