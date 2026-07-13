using FluentValidation;

namespace AIWorkHub.Application.Features.Kanban.MoveTask;

public sealed class MoveTaskValidator
    : AbstractValidator<MoveTaskRequest>
{
    public MoveTaskValidator()
    {
        RuleFor(x => x.Order)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Status)
            .IsInEnum();
    }
}
