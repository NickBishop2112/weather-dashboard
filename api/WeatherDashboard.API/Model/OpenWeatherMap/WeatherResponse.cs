using System.Text.Json.Serialization;

namespace WeatherDashboard.API.Model.OpenWeatherMap;

public class WeatherResponse
{
    [JsonPropertyName("current")]
    public required CurrentWeather Current { get; init; }
}