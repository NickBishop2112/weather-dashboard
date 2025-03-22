using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using WeatherDashboard.API.Interfaces;
using WeatherDashboard.API.Model;
using WeatherDashboard.API.Model.OpenWeatherMap;
using WeatherDashboard.API.Services;
using Xunit;

namespace WeatherDashboard.API.Tests.Services;

public class ForecastServicesTests
{
    private readonly Fixture _fixture;
    private readonly ForecastService _forecastService;
    private readonly Mock<IHttpClient> _httpClient;
    private readonly MemoryCache _cache;

    public ForecastServicesTests()
    {
        _fixture = new Fixture();
        
        var apiSettingOption = new Mock<IOptions<ApiSettings>>();
        var apiSettings = new ApiSettings { ApiKey = _fixture.Create<string>() };
        apiSettingOption.Setup(o => o.Value).Returns(apiSettings);
        
        _httpClient = new Mock<IHttpClient>();
        _cache = new MemoryCache(new MemoryCacheOptions());
        _forecastService = new ForecastService(_httpClient.Object, apiSettingOption.Object, _cache);
    }
    
    [Fact]
    public async Task GetForecast_DetailsFoundFromCache()
    {
        // Arrange
        var location = _fixture.Create<string>();
        var weatherForecast = _fixture.Create<WeatherForecast>();
        _cache.Set($"WeatherForecast_{location}", weatherForecast);
        
        // Act
        var result = await _forecastService.GetForecast(location);
    
        // Assert
        result.Should().BeEquivalentTo([weatherForecast]);
        _httpClient.Verify(x => x.GetResponseBody<List<GeoData>>(It.IsNotNull<string>()), Times.Never);
        _httpClient.Verify(x => x.GetResponseBody<WeatherResponse>(It.IsNotNull<string>()), Times.Never);

        _httpClient.VerifyAll();
    }
    
    [Fact]
    public async Task GetForecast_DetailsFound()
    {
        // Arrange
        var location = _fixture.Create<string>();
        var weatherResponse = new WeatherResponse { Current = _fixture.Create<CurrentWeather>() }; 
        var geoData = _fixture.Create<GeoData>();
        
        _httpClient.Setup(x => x.GetResponseBody<List<GeoData>>(It.IsNotNull<string>())).ReturnsAsync([geoData]);
        _httpClient.Setup(x => x.GetResponseBody<WeatherResponse>(It.IsNotNull<string>())).ReturnsAsync(weatherResponse);
        
        // Act
        var result = await _forecastService.GetForecast(location);
    
        // Assert
        result.Should().NotBeEmpty();
        _httpClient.VerifyAll();
    }
    
    [Fact]
    public async Task GetForecast_DetailsNotFound()
    {
        // Arrange
        var location = _fixture.Create<string>();
        var weatherResponse = new WeatherResponse { Current = _fixture.Create<CurrentWeather>() }; 
            
        _httpClient.Setup(x => x.GetResponseBody<List<GeoData>>(It.IsNotNull<string>())).ReturnsAsync([]);
        _httpClient.Setup(x => x.GetResponseBody<WeatherResponse>(It.IsNotNull<string>())).ReturnsAsync(weatherResponse);
        
        // Act
        var result = await _forecastService.GetForecast(location);
    
        // Assert
        result.Should().BeEmpty();
        _httpClient.Verify(x => x.GetResponseBody<WeatherResponse>(It.IsNotNull<string>()), Times.Never);
    }
}