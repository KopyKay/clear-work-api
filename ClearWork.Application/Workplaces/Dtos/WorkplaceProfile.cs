using AutoMapper;
using ClearWork.Domain.Entities;

namespace ClearWork.Application.Workplaces.Dtos;

public class WorkplaceProfile : Profile
{
    public WorkplaceProfile()
    {
        CreateMap<Workplace, WorkplaceDto>();
    }
}