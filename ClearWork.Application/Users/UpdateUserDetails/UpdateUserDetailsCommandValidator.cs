using FluentValidation;

namespace ClearWork.Application.Users.UpdateUserDetails;

public class UpdateUserDetailsCommandValidator : AbstractValidator<UpdateUserDetailsCommand>
{
    private const string NameRegex = @"^\p{L}+$";
    private const int MaxNameLenght = 30;

    public UpdateUserDetailsCommandValidator()
    {
        RuleFor(dto => dto.FirstName)
            .MaximumLength(MaxNameLenght)
            .WithMessage($"First name cannot exceed {MaxNameLenght} characters.")
            .Matches(NameRegex)
            .WithMessage("First name can only contain letters.")
            .When(dto => !string.IsNullOrEmpty(dto.FirstName));

        
        RuleFor(dto => dto.LastName)
            .MaximumLength(MaxNameLenght)
            .WithMessage($"Last name cannot exceed {MaxNameLenght} characters.")
            .Matches(NameRegex)
            .WithMessage("Last name can only contain letters.")
            .When(dto => !string.IsNullOrEmpty(dto.LastName));
        
        RuleFor(dto => dto.DisabilityLevel)
            .IsInEnum()
            .WithMessage("Invalid disability level.");
    }
}