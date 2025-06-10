using HaulageSystem.Application.Configuration;
using HaulageSystem.Application.Domain.Services;

namespace HaulageSystem.Application.Factories;

public interface IApiClientFactory
{
    ExtendedHttpClient CreateApiClient(string clientName);
}