using HaulageSystem.Application.Domain.Entities.Users;

namespace HaulageSystem.Application.Domain.Interfaces.Services;

public interface IUserService
{
    Task<string> GetCurrentUsername();
    Task<GetUserInfoResponse> GetUserInfo(string username);
    Task<string> GetFullName();
    Task<string> GetFullName(string username);
    Task<string> GetEmailAddress(string username);
}