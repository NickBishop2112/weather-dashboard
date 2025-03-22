using System.Text.Json;
using WeatherDashboard.API.Interfaces;

namespace WeatherDashboard.API.Services;

public class HttpClient(IHttpClientFactory httpClientFactory) : IHttpClient
{
    public async Task<T> GetResponseBody<T>(string apiUrl)
    {
        using var client = httpClientFactory.CreateClient();
        var response = await client.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();
            
        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseBody) ?? throw new InvalidOperationException();
    }
}