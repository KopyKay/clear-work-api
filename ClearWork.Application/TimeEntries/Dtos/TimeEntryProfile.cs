using AutoMapper;
using ClearWork.Domain.Entities;
using ClearWork.Domain.OwnedTypes;

namespace ClearWork.Application.TimeEntries.Dtos;

public class TimeEntryProfile : Profile
{
    public TimeEntryProfile()
    {
        CreateMap<TimeEntry, TimeEntryDto>()
            .ForMember(dto => dto.EntryType, opt =>
                opt.MapFrom(src => src.GetType().Name))
            .ForMember(dto => dto.HasAudioNote, opt =>
                opt.MapFrom(src => src.AudioNote != null))
            .ForMember(dto => dto.GrossSalary, opt =>
                opt.MapFrom(src => src.Calculation.GrossSalary))
            .ForMember(dto => dto.NetSalary, opt =>
                opt.MapFrom(src => src.Calculation.NetSalary))
            .Include<WorkEntry, WorkEntryDto>()
            .Include<LeaveEntry, LeaveEntryDto>();
        
        CreateMap<WorkEntry, WorkEntryDto>()
            .IncludeBase<TimeEntry, TimeEntryDto>();

        CreateMap<LeaveEntry, LeaveEntryDto>()
            .IncludeBase<TimeEntry, TimeEntryDto>();
        
        CreateMap<AudioNote, AudioNoteDto>();
        
        CreateMap<DailyBusinessTripDetail, DailyBusinessTripDetailDto>();
    }
}