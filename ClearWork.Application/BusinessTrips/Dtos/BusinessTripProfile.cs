using AutoMapper;
using ClearWork.Domain.Entities;

namespace ClearWork.Application.BusinessTrips.Dtos;

public class BusinessTripProfile : Profile
{
    public BusinessTripProfile()
    {
        CreateMap<BusinessTrip, BusinessTripDto>();
    }
}