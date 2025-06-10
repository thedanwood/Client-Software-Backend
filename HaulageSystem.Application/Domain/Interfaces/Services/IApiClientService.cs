namespace HaulageSystem.Application.Domain.Interfaces.Services;

public interface IApiClientService
{
    Task<T> GetAsync<T>(string requestUri);
}