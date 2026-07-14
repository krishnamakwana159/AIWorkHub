using FluentValidation;

namespace AIWorkHub.Application.Features.AI.GenerateTaskDescription;

public sealed class GenerateTaskDescriptionCommandValidator
    : AbstractValidator<GenerateTaskDescriptionCommand>
{
    public GenerateTaskDescriptionCommandValidator()
    {
        RuleFor(x => x.Request.Title)
            .NotEmpty()
            .MaximumLength(200);
    }
}
