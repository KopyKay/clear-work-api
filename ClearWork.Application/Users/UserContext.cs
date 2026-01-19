using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace ClearWork.Application.Users;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor.HttpContext?.User;
        
        if (user is null)
            throw new InvalidOperationException("User context is not present.");

        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return null;
        
        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        
        return new CurrentUser(userId);
    }
}