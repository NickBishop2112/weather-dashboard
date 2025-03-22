namespace WeatherDashboard.API.Interfaces;

public interface IHttpClient
{
    Task<T> GetResponseBody<T>(string apiUrl);
}