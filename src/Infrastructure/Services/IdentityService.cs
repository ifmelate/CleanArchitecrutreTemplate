using Microsoft.AspNetCore.Authorization;
using ProjectName.ServiceName.Application.Common.Interfaces;

namespace ProjectName.ServiceName.Infrastructure.Services;

public class IdentityService : IIdentityService
{
#pragma warning disable S4487
    private readonly IAuthorizationService _authorizationService;
#pragma warning restore S4487

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
        return Task.FromResult(true);
    }

}
