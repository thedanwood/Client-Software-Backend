using HaulageSystem.Application.Configuration;
using HaulageSystem.Application.Domain.Services;

namespace HaulageSystem.Application.Factories;

public class ApiClientFactory : IApiClientFactory
{
    private readonly IHttpClientFactory _clientFactory;

    public ApiClientFactory(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public ExtendedHttpClient CreateApiClient(string clientName)
    {
        var httpClient = _clientFactory.CreateClient(clientName);

        return new ExtendedHttpClient(httpClient);
    }
}