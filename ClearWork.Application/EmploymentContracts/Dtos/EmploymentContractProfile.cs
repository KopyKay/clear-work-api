using AutoMapper;
using ClearWork.Domain.Entities;

namespace ClearWork.Application.EmploymentContracts.Dtos;

public class EmploymentContractProfile : Profile
{
    public EmploymentContractProfile()
    {
        CreateMap<EmploymentContract, EmploymentContractDto>();
    }
}