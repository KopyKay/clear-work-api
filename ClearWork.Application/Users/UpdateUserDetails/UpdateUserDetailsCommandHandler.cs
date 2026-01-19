using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.Users.UpdateUserDetails;

public class UpdateUserDetailsCommandHandler
(
    ILogger<UpdateUserDetailsCommandHandler> logger,
    UserManager<User> userManager,
    IUserContext userContext
)
: IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()!;
        
        logger.LogInformation("Updating user details for user with id [{UserId}]", user.Id);
        
        var dbUser = await userManager.FindByIdAsync(user.Id)
                     ?? throw new NotFoundException(nameof(User), user.Id);
        
        dbUser.FirstName = request.FirstName ?? dbUser.FirstName;
        dbUser.LastName = request.LastName ?? dbUser.LastName;
        dbUser.DateOfBirth = request.DateOfBirth ?? dbUser.DateOfBirth;
        dbUser.IsStudent = request.IsStudent ?? dbUser.IsStudent;
        dbUser.DisabilityLevel = request.DisabilityLevel;
        
        await userManager.UpdateAsync(dbUser);
    }
}