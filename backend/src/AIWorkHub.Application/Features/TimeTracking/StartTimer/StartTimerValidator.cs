using FluentValidation;

namespace AIWorkHub.Application.Features.TimeTracking.StartTimer;

public sealed class StartTimerValidator
    : AbstractValidator<StartTimerRequest>
{
    public StartTimerValidator()
    {
        RuleFor(x => x.WorkTaskId)
            .NotEmpty();
    }
}
