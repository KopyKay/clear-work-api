using ClearWork.Application.AppSettings.Dtos;
using MediatR;

namespace ClearWork.Application.AppSettings.Queries.GetUserAppSettings;

public record GetUserAppSettingsQuery : IRequest<AppSettingDto?>;