using FluentValidation;

namespace ClearWork.Application.TimeEntries.Commands.UpdateEmploymentContractWorkEntry;

public class UpdateEmploymentContractWorkEntryCommandValidator : AbstractValidator<UpdateEmploymentContractWorkEntryCommand>
{
    private const int MaxTextNoteLength = 2500;
    
    public UpdateEmploymentContractWorkEntryCommandValidator()
    {
        When(command => !string.IsNullOrEmpty(command.TextNote), () =>
        {
            RuleFor(command => command.TextNote)
                .MaximumLength(MaxTextNoteLength)
                .WithMessage($"Text note cannot exceed {MaxTextNoteLength} characters.");
        });

        RuleFor(command => command)
            .Must(command => !(command.BusinessTripId.HasValue && command.ClearBusinessTrip == true))
            .WithMessage("Cannot set and clear business trip at the same time.");

        RuleFor(command => command)
            .Must(command => !(command.DailyBusinessTripDetail is not null && command.ClearDailyBusinessTripDetail == true))
            .WithMessage("Cannot set and clear daily business trip detail at the same time.");

        When(command => command.DailyBusinessTripDetail is not null, () =>
        {
            When(command => command.DailyBusinessTripDetail!.ActualAccommodationCostThisDay.HasValue, () =>
            {
                RuleFor(command => command.DailyBusinessTripDetail!.ActualAccommodationCostThisDay!.Value)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Actual accommodation cost must be greater than or equal to 0.");
            });
        });
    }
}