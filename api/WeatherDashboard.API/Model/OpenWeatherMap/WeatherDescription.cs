using System.Text.Json.Serialization;

namespace WeatherDashboard.API.Model.OpenWeatherMap;

public class WeatherDescription
{
    [JsonPropertyName("icon")]
    public required string Icon { get; init; }
}