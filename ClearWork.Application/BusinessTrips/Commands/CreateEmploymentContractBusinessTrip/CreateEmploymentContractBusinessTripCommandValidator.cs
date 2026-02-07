using ClearWork.Domain.Constants;
using ClearWork.Domain.Enums;
using FluentValidation;

namespace ClearWork.Application.BusinessTrips.Commands.CreateEmploymentContractBusinessTrip;

public class CreateEmploymentContractBusinessTripCommandValidator : AbstractValidator<CreateEmploymentContractBusinessTripCommand>
{
    public CreateEmploymentContractBusinessTripCommandValidator()
    {
        RuleFor(command => command.StartDateTime)
            .NotEmpty()
            .WithMessage("Start date is required.");

        When(command => command.EndDateTime.HasValue, () =>
        {
            RuleFor(command => command)
                .Must(command => command.EndDateTime > command.StartDateTime)
                .WithMessage("End date must be after start date.");
        });

        RuleFor(command => command.TripType)
            .IsInEnum()
            .WithMessage("Invalid trip type.");

        RuleFor(command => command.DestinationCountry)
            .NotEmpty()
            .WithMessage("Destination country is required.")
            .Length(3)
            .WithMessage("Destination country must be a 3-letter ISO code.");

        RuleFor(command => command)
            .Must(command => IsTripTypeConsistentWithCountry(command.TripType, command.DestinationCountry))
            .WithMessage("Trip type must be consistent with destination country. " +
                         "Use 'Domestic' for Poland (POL) and 'International' for other countries.");

        RuleFor(command => command.AccommodationType)
            .IsInEnum()
            .WithMessage("Invalid accommodation type.");

        RuleFor(command => command.TransportType)
            .IsInEnum()
            .WithMessage("Invalid transport type.");
        
        When(command => command.KilometersDriven.HasValue, () =>
        {
            RuleFor(command => command.KilometersDriven!.Value)
                .GreaterThan(0)
                .WithMessage("Kilometers driven must be greater than 0.");

            RuleFor(command => command.TransportType)
                .Must(t => t is TransportType.PrivateCar or TransportType.Motorcycle or TransportType.Moped)
                .WithMessage("Kilometers driven can only be specified for private car, motorcycle, or moped.");
        });

        When(command => command.CarEngineCapacity.HasValue, () =>
        {
            RuleFor(command => command.CarEngineCapacity!.Value)
                .GreaterThan(0)
                .WithMessage("Car engine capacity must be greater than 0.");

            RuleFor(command => command.TransportType)
                .Equal(TransportType.PrivateCar)
                .WithMessage("Car engine capacity can only be specified for private car.");
        });

        When(command => command.ActualTransportCost.HasValue, () =>
        {
            RuleFor(command => command.ActualTransportCost!.Value)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Actual transport cost must be greater than or equal to 0.");
        });
    }
    
    private static bool IsTripTypeConsistentWithCountry(BusinessTripType tripType, string? country)
    {
        if (string.IsNullOrEmpty(country))
            return true;

        var isPoland = country.Equals(CountryCodes.Poland, StringComparison.OrdinalIgnoreCase);
        
        return tripType switch
        {
            BusinessTripType.Domestic => isPoland,
            BusinessTripType.International => !isPoland,
            _ => false
        };
    }
}