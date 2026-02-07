using FluentValidation;

namespace ClearWork.Application.BusinessTrips.Commands.UpdateEmploymentContractBusinessTrip;

public class UpdateEmploymentContractBusinessTripCommandValidator : AbstractValidator<UpdateEmploymentContractBusinessTripCommand>
{
    public UpdateEmploymentContractBusinessTripCommandValidator()
    {
        When(command => command.TripType.HasValue, () =>
        {
            RuleFor(command => command.TripType!.Value)
                .IsInEnum()
                .WithMessage("Invalid trip type.");
        });

        When(command => !string.IsNullOrEmpty(command.DestinationCountry), () =>
        {
            RuleFor(command => command.DestinationCountry!)
                .Length(3)
                .WithMessage("Destination country must be a 3-letter ISO code.");
        });

        When(command => command.AccommodationType.HasValue, () =>
        {
            RuleFor(command => command.AccommodationType!.Value)
                .IsInEnum()
                .WithMessage("Invalid accommodation type.");
        });

        When(command => command.TransportType.HasValue, () =>
        {
            RuleFor(command => command.TransportType!.Value)
                .IsInEnum()
                .WithMessage("Invalid transport type.");
        });

        When(command => command.KilometersDriven.HasValue, () =>
        {
            RuleFor(command => command.KilometersDriven!.Value)
                .GreaterThan(0)
                .WithMessage("Kilometers driven must be greater than 0.");
        });

        When(command => command.CarEngineCapacity.HasValue, () =>
        {
            RuleFor(command => command.CarEngineCapacity!.Value)
                .GreaterThan(0)
                .WithMessage("Car engine capacity must be greater than 0.");
        });

        When(command => command.ActualTransportCost.HasValue, () =>
        {
            RuleFor(command => command.ActualTransportCost!.Value)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Actual transport cost must be greater than or equal to 0.");
        });
    }
}