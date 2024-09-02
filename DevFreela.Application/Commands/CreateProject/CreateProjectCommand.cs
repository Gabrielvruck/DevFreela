using DevFreela.Application.InputModels;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateProjectCommand : NewProjectInputModel, IRequest<int>
    {
    }
}
