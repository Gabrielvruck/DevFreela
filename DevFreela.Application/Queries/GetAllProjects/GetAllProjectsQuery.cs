﻿using DevFreela.Application.ViewModels;
using DevFreela.Core.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectsQuery : IRequest<PaginationResult<ProjectViewModel>>
    {
        public string Title { get; set; } = string.Empty;
        public int Page { get; set; } = 1;
    }
}
