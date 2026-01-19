using ClearWork.Domain.Enums;
using MediatR;

namespace ClearWork.Application.Users.UpdateUserDetails;

public record UpdateUserDetailsCommand
(
    string? FirstName,
    string? LastName,
    DateOnly? DateOfBirth,
    bool? IsStudent,
    DisabilityLevel? DisabilityLevel
) 
: IRequest;