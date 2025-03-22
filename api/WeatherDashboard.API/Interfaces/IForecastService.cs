using WeatherDashboard.API.Model;

namespace WeatherDashboard.API.Interfaces;

public interface IForecastService
{
    Task<IEnumerable<WeatherForecast>> GetForecast(string location);
}