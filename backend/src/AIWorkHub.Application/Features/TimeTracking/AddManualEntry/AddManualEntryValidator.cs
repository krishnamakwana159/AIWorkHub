using FluentValidation;

namespace AIWorkHub.Application.Features.TimeTracking.AddManualEntry;

public sealed class AddManualEntryValidator
    : AbstractValidator<AddManualEntryRequest>
{
    public AddManualEntryValidator()
    {
        RuleFor(x => x.WorkTaskId)
            .NotEmpty();

        RuleFor(x => x.EndTimeUtc)
            .GreaterThan(x => x.StartTimeUtc);

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}
