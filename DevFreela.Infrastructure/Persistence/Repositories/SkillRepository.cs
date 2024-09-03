using Dapper;
using DevFreela.Core.Dtos;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly string _connectionString;
        public SkillRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<List<SkillDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync(cancellationToken);
                var script = "SELECT Id, Description FROM Skills";
                var skills = await sqlConnection.QueryAsync<SkillDto>(script);

                return skills.ToList();
            }

            // COM EF CORE
            //var skills = _dbContext.Skills;

            //var skillsViewModel = skills
            //    .Select(s => new SkillViewModel(s.Id, s.Description))
            //    .ToList();

            //return skillsViewModel;
        }
    }
}
