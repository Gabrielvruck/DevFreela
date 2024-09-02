using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>>
    {
        private readonly DevFreelaDbContext dbContext;
        public GetAllProjectsQueryHandler(DevFreelaDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<List<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = dbContext.Projects;

            var projectsViewModel = await projects
                .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
                .ToListAsync(cancellationToken: cancellationToken);

            return projectsViewModel;
        }
    }
}
