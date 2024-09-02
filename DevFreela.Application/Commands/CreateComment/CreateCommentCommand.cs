using DevFreela.Application.InputModels;
using MediatR;

namespace DevFreela.Application.Commands.CreateComment
{
    public class CreateCommentCommand : CreateCommentInputModel, IRequest<Unit>
    {
    }
}
