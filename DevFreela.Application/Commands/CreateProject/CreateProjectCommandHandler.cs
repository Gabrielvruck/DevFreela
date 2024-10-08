﻿using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(request.Title, request.Description, request.IdClient, request.IdFreelancer, request.TotalCost);
            project.Comments.Add(new ProjectComment("Project was created.", project.Id, request.IdClient));
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.Projects.AddAsync(project, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            await _unitOfWork.Skills.AddSkillFromProject(project, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return project.Id;
        }
    }
}
