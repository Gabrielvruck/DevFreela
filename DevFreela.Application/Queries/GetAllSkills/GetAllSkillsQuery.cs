using DevFreela.Core.Dtos;
using DevFreela.Core.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsQuery : IRequest<PaginationResult<SkillDto>>
    {
        public string Query { get; set; } = string.Empty;
        public int Page { get; set; } = 1;
    }
}
