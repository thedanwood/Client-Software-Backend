using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using HaulageSystem.Application.Domain.Entities.Users;
using HaulageSystem.Application.Domain.Interfaces.Services;
using HaulageSystem.Application.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Azure;

namespace HaulageSystem.Application.Domain.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<string> GetCurrentUsername()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user?.Identity?.IsAuthenticated ?? false)
        {
            return user.Identity.Name;
        }
        return null;
    }
    public async Task<GetUserInfoResponse> GetUserInfo(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        return new()
        {
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }
    public async Task<string> GetFullName(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        return user != null ? $"{user.FirstName} {user.LastName}" : null;
    }
    
    public async Task<string> GetFullName()
    {
        var user = await _userManager.FindByNameAsync(await GetCurrentUsername());
        return user != null ? $"{user.FirstName} {user.LastName}" : null;
    }
    
    public async Task<string> GetEmailAddress(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        return user.Email;
    }
}