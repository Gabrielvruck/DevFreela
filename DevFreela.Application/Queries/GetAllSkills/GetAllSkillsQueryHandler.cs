using DevFreela.Core.Dtos;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, PaginationResult<SkillDto>>
    {
        private readonly ISkillRepository _skillRepository;
        public GetAllSkillsQueryHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
        }

        public async Task<PaginationResult<SkillDto>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skillPaginationResult = await _skillRepository.GetAllAsync(request.Query, request.Page, cancellationToken);

            var skillsViewModel = skillPaginationResult
               .Data
               .Select(s => new SkillDto { Id = s.Id, Description = s.Description })
               .ToList();

            var paginationResult = new PaginationResult<SkillDto>(
            skillPaginationResult.Page,
            skillPaginationResult.TotalPages,
            skillPaginationResult.PageSize,
            skillPaginationResult.ItemsCount,
            skillsViewModel
            );

            return paginationResult;
        }
    }
}
