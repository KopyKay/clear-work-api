using AutoMapper;
using ClearWork.Application.TimeEntries.Commands.CreateEmploymentContractLeaveEntry;
using ClearWork.Application.TimeEntries.Commands.CreateEmploymentContractWorkEntry;
using ClearWork.Application.TimeEntries.Commands.UpdateEmploymentContractLeaveEntry;
using ClearWork.Application.TimeEntries.Commands.UpdateEmploymentContractWorkEntry;
using ClearWork.Domain.Entities;
using ClearWork.Domain.OwnedTypes;

namespace ClearWork.Application.TimeEntries.Dtos;

public class TimeEntryProfile : Profile
{
    public TimeEntryProfile()
    {
        // === Read DTOs ===
        
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
        
        // === Create DTOs ===
        
        CreateMap<CreateEmploymentContractWorkEntryCommand, WorkEntry>()
            .ForMember(dest => dest.StartDateTime, opt =>
                opt.MapFrom(src => src.StartDateTime.ToUniversalTime()))
            .ForMember(dest => dest.EndDateTime, opt =>
                opt.MapFrom(src => src.EndDateTime.ToUniversalTime()))
            .ForMember(dest => dest.DailyBusinessTripDetail, opt =>
                opt.MapFrom(src => src.DailyBusinessTripDetail));
        
        CreateMap<CreateDailyBusinessTripDetailDto, DailyBusinessTripDetail>();
        
        CreateMap<CreateEmploymentContractLeaveEntryCommand, LeaveEntry>()
            .ForMember(dest => dest.StartDateTime, opt =>
                opt.MapFrom(src => src.StartDateTime.ToUniversalTime()))
            .ForMember(dest => dest.EndDateTime, opt =>
                opt.MapFrom(src => src.EndDateTime.ToUniversalTime()));
        
        // === Update DTOs ===
        
        CreateMap<UpdateEmploymentContractWorkEntryCommand, WorkEntry>()
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
            .ForMember(dest => dest.TextNote, opt =>
                opt.Condition(src => src.TextNote != null))
            .ForMember(dest => dest.BusinessTripId, opt =>
                opt.Condition(src => src.BusinessTripId.HasValue))
            .ForMember(dest => dest.DailyBusinessTripDetail, opt =>
                opt.Condition(src => src.DailyBusinessTripDetail != null))
            .ForMember(dest => dest.ModifiedAt, opt =>
                opt.MapFrom(_ => DateTimeOffset.UtcNow));
        
        CreateMap<UpdateDailyBusinessTripDetailDto, DailyBusinessTripDetail>()
            .ForMember(dest => dest.ProvidedBreakfast, opt =>
                opt.Condition(src => src.ProvidedBreakfast.HasValue))
            .ForMember(dest => dest.ProvidedLunch, opt =>
                opt.Condition(src => src.ProvidedLunch.HasValue))
            .ForMember(dest => dest.ProvidedDinner, opt =>
                opt.Condition(src => src.ProvidedDinner.HasValue))
            .ForMember(dest => dest.HasOvernightStay, opt =>
                opt.Condition(src => src.HasOvernightStay.HasValue))
            .ForMember(dest => dest.ActualAccommodationCostThisDay, opt =>
                opt.Condition(src => src.ActualAccommodationCostThisDay.HasValue));
        
        CreateMap<UpdateEmploymentContractLeaveEntryCommand, LeaveEntry>()
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
            .ForMember(dest => dest.TextNote, opt =>
                opt.Condition(src => src.TextNote != null))
            .ForMember(dest => dest.LeaveType, opt =>
                opt.Condition(src => src.LeaveType.HasValue))
            .ForMember(dest => dest.WorkingDays, opt =>
                opt.Condition(src => src.WorkingDays.HasValue))
            .ForMember(dest => dest.ModifiedAt, opt =>
                opt.MapFrom(_ => DateTimeOffset.UtcNow));
    }
}