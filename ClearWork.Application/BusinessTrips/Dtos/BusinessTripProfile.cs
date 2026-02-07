using AutoMapper;
using ClearWork.Application.BusinessTrips.Commands.CreateEmploymentContractBusinessTrip;
using ClearWork.Application.BusinessTrips.Commands.UpdateEmploymentContractBusinessTrip;
using ClearWork.Domain.Entities;

namespace ClearWork.Application.BusinessTrips.Dtos;

public class BusinessTripProfile : Profile
{
    public BusinessTripProfile()
    {
        CreateMap<BusinessTrip, BusinessTripDto>();
        
        CreateMap<CreateEmploymentContractBusinessTripCommand, BusinessTrip>()
            .ForMember(dest => dest.StartDateTime, opt =>
                opt.MapFrom(src => src.StartDateTime.ToUniversalTime()))
            .ForMember(dest => dest.EndDateTime, opt =>
                opt.MapFrom(src => src.EndDateTime.HasValue 
                    ? src.EndDateTime.Value.ToUniversalTime() 
                    : (DateTimeOffset?)null))
            .ForMember(dest => dest.DestinationCountry, opt =>
                opt.MapFrom(src => src.DestinationCountry.ToUpperInvariant()));
        
        CreateMap<UpdateEmploymentContractBusinessTripCommand, BusinessTrip>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.EmploymentContractId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.StartDateTime, opt =>
            {
                opt.PreCondition(src => src.StartDateTime.HasValue);
                opt.MapFrom(src => src.StartDateTime!.Value.ToUniversalTime());
            })
            .ForMember(dest => dest.EndDateTime, opt =>
            {
                opt.PreCondition(src => src.EndDateTime.HasValue);
                opt.MapFrom(src => src.EndDateTime!.Value.ToUniversalTime());
            })
            .ForMember(dest => dest.TripType, opt =>
                opt.Condition(src => src.TripType.HasValue))
            .ForMember(dest => dest.DestinationCountry, opt =>
            {
                opt.PreCondition(src => !string.IsNullOrEmpty(src.DestinationCountry));
                opt.MapFrom(src => src.DestinationCountry!.ToUpperInvariant());
            })
            .ForMember(dest => dest.AccommodationType, opt =>
                opt.Condition(src => src.AccommodationType.HasValue))
            .ForMember(dest => dest.TransportType, opt =>
                opt.Condition(src => src.TransportType.HasValue))
            .ForMember(dest => dest.KilometersDriven, opt =>
                opt.Condition(src => src.KilometersDriven.HasValue))
            .ForMember(dest => dest.CarEngineCapacity, opt =>
                opt.Condition(src => src.CarEngineCapacity.HasValue))
            .ForMember(dest => dest.ActualTransportCost, opt =>
                opt.Condition(src => src.ActualTransportCost.HasValue))
            .ForMember(dest => dest.UseLocalTransportAllowance, opt =>
                opt.Condition(src => src.UseLocalTransportAllowance.HasValue));
    }
}