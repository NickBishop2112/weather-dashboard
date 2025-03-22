using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Serilog;
using WeatherDashboard.API.Interfaces;
using WeatherDashboard.API.Model;
using WeatherDashboard.API.Model.OpenWeatherMap;

namespace WeatherDashboard.API.Services;

public class ForecastService(IHttpClient httpClient, IOptions<ApiSettings> options, IMemoryCache memoryCache)
    : IForecastService
{
    private readonly string _apiKey = options.Value.ApiKey;
    
    public async Task<IEnumerable<WeatherForecast>> GetForecast(string location)
    {
        string cacheKey = $"WeatherForecast_{location}";
     
        Log.Information($"Checking cache for location: {location}");
        
        WeatherForecast? cachedForecast;
        if (memoryCache.TryGetValue(cacheKey, out cachedForecast))
        {
            return new List<WeatherForecast> {cachedForecast ?? throw new InvalidOperationException()};
        }
        
        Log.Information($"Get geo data for location: {location}");
        var geoData = await GetGeoData(location);

        if (geoData is not null)
        {
            Log.Information($"Get forecast for : {location}");
            var weatherForecast = await GetDetails(geoData);
            memoryCache.Set(cacheKey, weatherForecast, TimeSpan.FromMinutes(2));
            return [weatherForecast];
        }
        
        Log.Information($"No location found: {location}");
        return new List<WeatherForecast>();
    }

    private async Task<WeatherForecast> GetDetails(GeoData geoData)
    {
        var apiUrl = $"https://api.openweathermap.org/data/3.0/onecall?lat={geoData.Lattitude}&lon={geoData.Longitude}&appid={_apiKey}&units=metric&exclude=minutely,hourly";
            
        var weatherData = await httpClient.GetResponseBody<WeatherResponse>(apiUrl);
        return new(weatherData!.Current.Temperature, weatherData.Current.Humidity, weatherData.Current.WindSpeed,
            weatherData.Current.Weather[0].Icon);
    }

    private async Task<GeoData?> GetGeoData(string city)
    {
        var geoApiUrl = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=1&appid={_apiKey}";
        
        var geoData = await httpClient.GetResponseBody<List<GeoData>>(geoApiUrl);

        return geoData?.Count != 0 ? geoData?[0] : null;
    }
}