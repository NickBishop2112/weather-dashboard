using System.Text.Json.Serialization;

namespace WeatherDashboard.API.Model.OpenWeatherMap;

public class CurrentWeather
{
    [JsonPropertyName("temp")]
    public required double Temperature { get; init; }
    
    [JsonPropertyName("humidity")]
    public required int Humidity { get; init; }
    
    [JsonPropertyName("wind_speed")]
    public required double WindSpeed { get; init; }
    
    [JsonPropertyName("weather")]
    public required WeatherDescription[] Weather { get; init; }
}