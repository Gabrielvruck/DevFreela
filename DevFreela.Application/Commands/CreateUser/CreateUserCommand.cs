using DevFreela.Application.InputModels;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserCommand : CreateUserInputModel, IRequest<int>
    {
    }
}
