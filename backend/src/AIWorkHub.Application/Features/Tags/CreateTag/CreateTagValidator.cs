using AIWorkHub.Application.Features.Tags.DTOs;
using FluentValidation;

namespace AIWorkHub.Application.Features.Tags.CreateTag;

public sealed class CreateTagValidator : AbstractValidator<CreateTagRequest>
{
    public CreateTagValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Color)
            .NotEmpty()
            .Matches("^#[0-9A-Fa-f]{6}$")
            .WithMessage("Color must be a hex value like #64748b.");
    }
}
