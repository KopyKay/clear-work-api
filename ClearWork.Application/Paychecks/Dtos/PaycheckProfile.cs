using AutoMapper;
using ClearWork.Domain.Entities;

namespace ClearWork.Application.Paychecks.Dtos;

public class PaycheckProfile : Profile
{
    public PaycheckProfile()
    {
        CreateMap<Paycheck, PaycheckDto>();
    }
}