using DevFreela.Application.InputModels;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject
{
    public class UpdateProjectCommand : UpdateProjectInputModel, IRequest<Unit>
    {
    }
}
