using Microsoft.AspNetCore.Authorization;
using ProjectName.ServiceName.Application.Common.Interfaces;

namespace ProjectName.ServiceName.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(
        IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public Task<string> GetUserNameAsync(string userId)
    {
        return Task.FromResult("TestUser");
    }
    public Task<bool> IsInRoleAsync(string userId, string role)
    {
        return Task.FromResult(true);
    }

    public Task<bool> AuthorizeAsync(string userId, string policyName)
    {
    
       // var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        //return result.Succeeded;
        return Task.FromResult(true);
    }

}
