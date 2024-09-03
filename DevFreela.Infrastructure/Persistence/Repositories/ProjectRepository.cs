using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task AddAsync(Project project, CancellationToken cancellationToken)
        {
            await _dbContext.Projects.AddAsync(project, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddCommentAsync(ProjectComment projectComment, CancellationToken cancellationToken)
        {
            await _dbContext.ProjectComments.AddAsync(projectComment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Project>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Projects.ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Project> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<Project> GetDetailsByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Projects
               .Include(p => p.Client)
               .Include(p => p.Freelancer)
               .SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
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
