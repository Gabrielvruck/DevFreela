using DevFreela.Application.Commands.CreateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateProjectCommandvalidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandvalidator()
        {
            RuleFor(p => p.Description)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .WithMessage("O campo deve ter pelo menos 3 caracteres.")
                .MaximumLength(255)
                .WithMessage("Tamanho máximo de Descriçao é de 255 caracteres.");

            RuleFor(p => p.Title)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .WithMessage("O campo deve ter pelo menos 3 caracteres.")
                .MaximumLength(30)
                .WithMessage("Tamanho máximo de Título é de 30 caracteres");

            RuleFor(p => p.TotalCost)
                .GreaterThanOrEqualTo(100)
                .WithMessage("O campo deve ter pelo menos 3 dígitos.");
        }
    }
}
