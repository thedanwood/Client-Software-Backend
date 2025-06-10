using HaulageSystem.Application.Models.Requests;

namespace HaulageSystem.Application.Domain.Interfaces.Services;

public interface IMailService
{
    Task<bool> SendEmail(SendEmailRequest request);
}