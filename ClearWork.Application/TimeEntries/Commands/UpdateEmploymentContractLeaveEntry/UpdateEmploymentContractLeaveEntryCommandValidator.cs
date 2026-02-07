using FluentValidation;

namespace ClearWork.Application.TimeEntries.Commands.UpdateEmploymentContractLeaveEntry;

public class UpdateEmploymentContractLeaveEntryCommandValidator : AbstractValidator<UpdateEmploymentContractLeaveEntryCommand>
{
    private const int MaxTextNoteLength = 2500;
    
    public UpdateEmploymentContractLeaveEntryCommandValidator()
    {
        When(command => !string.IsNullOrEmpty(command.TextNote), () =>
        {
            RuleFor(command => command.TextNote)
                .MaximumLength(MaxTextNoteLength)
                .WithMessage($"Text note cannot exceed {MaxTextNoteLength} characters.");
        });

        When(command => command.LeaveType.HasValue, () =>
        {
            RuleFor(command => command.LeaveType!.Value)
                .IsInEnum()
                .WithMessage("Invalid leave type.");
        });

        When(command => command.WorkingDays.HasValue, () =>
        {
            RuleFor(command => command.WorkingDays!.Value)
                .GreaterThan(0)
                .WithMessage("Working days must be greater than 0.");
        });
    }
}