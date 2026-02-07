using FluentValidation;

namespace ClearWork.Application.TimeEntries.Commands.CreateEmploymentContractLeaveEntry;

public class CreateEmploymentContractLeaveEntryCommandValidator : AbstractValidator<CreateEmploymentContractLeaveEntryCommand>
{
    private const int MaxTextNoteLength = 2500;
    
    public CreateEmploymentContractLeaveEntryCommandValidator()
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

        RuleFor(command => command.LeaveType)
            .IsInEnum()
            .WithMessage("Invalid leave type.");

        RuleFor(command => command.WorkingDays)
            .GreaterThan(0)
            .WithMessage("Working days must be greater than 0.");
    }
}