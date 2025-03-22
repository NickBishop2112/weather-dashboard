using System.Text.Json.Serialization;

namespace WeatherDashboard.API.Model.OpenWeatherMap;

public class GeoData  
{
    [JsonPropertyName("lat")]
    public required double Lattitude { get; init; }
    [JsonPropertyName("lon")]
    public required double Longitude { get; init; }
}