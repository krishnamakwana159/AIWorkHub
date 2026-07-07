using AIWorkHub.Application.Features.Projects.DTOs;
using FluentValidation;

namespace AIWorkHub.Application.Features.Projects.Create;

public sealed class CreateProjectValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Description)
            .MaximumLength(1000);

        RuleFor(x => x.Color)
            .MaximumLength(20);
    }
}
