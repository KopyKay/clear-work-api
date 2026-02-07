using FluentValidation;

namespace ClearWork.Application.TimeEntries.Commands.CreateEmploymentContractWorkEntry;

public class CreateEmploymentContractWorkEntryCommandValidator : AbstractValidator<CreateEmploymentContractWorkEntryCommand>
{
    private const int MaxTextNoteLength = 2500;
    
    public CreateEmploymentContractWorkEntryCommandValidator()
    {
        RuleFor(command => command.StartDateTime)
            .NotEmpty()
            .WithMessage("Start date is required.");

        RuleFor(command => command.EndDateTime)
            .NotEmpty()
            .WithMessage("End date is required.");

        RuleFor(command => command)
            .Must(command => command.EndDateTime > command.StartDateTime)
            .WithMessage("End date must be after start date.");

        When(command => !string.IsNullOrEmpty(command.TextNote), () =>
        {
            RuleFor(command => command.TextNote)
                .MaximumLength(MaxTextNoteLength)
                .WithMessage($"Text note cannot exceed {MaxTextNoteLength} characters.");
        });
        
        When(command => command.DailyBusinessTripDetail is not null && !command.BusinessTripId.HasValue, () =>
        {
            RuleFor(command => command.BusinessTripId)
                .NotNull()
                .WithMessage("Business trip ID is required when daily business trip details are provided.");
        });

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