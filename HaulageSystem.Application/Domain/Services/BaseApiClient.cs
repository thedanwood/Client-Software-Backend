using Newtonsoft.Json;

namespace HaulageSystem.Application.Domain.Services;

public class ExtendedHttpClient
{
    private readonly HttpClient _httpClient;
    public ExtendedHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> GetAsync<T>(string requestUri)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress+requestUri); 
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content);
    }
}