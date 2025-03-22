using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherDashboard.API.Controllers;
using WeatherDashboard.API.Interfaces;
using WeatherDashboard.API.Model;
using Xunit;
using Assert = Xunit.Assert;

namespace WeatherDashboard.API.Tests.Controllers;

public class ForecastControllerTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IForecastService> _forecastService;
    private readonly ForecastController _controller;

    public ForecastControllerTests()
    {
        _fixture = new Fixture();
        _forecastService = new Mock<IForecastService>();
        _controller = new ForecastController(_forecastService.Object);
    }
    
    [Fact]
    public async Task GetDetails_DetailsReturned()
    {
        // Arrange
        var weatherForecast = _fixture.CreateMany<WeatherForecast>(1);
        _forecastService.Setup(x => x.GetForecast("London")).ReturnsAsync(weatherForecast);

        // Act
        var result = await _controller.GetForecastDetails("London");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        okResult.Value.Should().BeEquivalentTo(weatherForecast);
        _forecastService.Verify(x => x.GetForecast("London"), Times.Once);
    }
    
    [Fact]
    public async Task GetDetails_ErrorThrown()
    {
        // Arrange
        _forecastService.Setup(x => x.GetForecast("London")).Throws<InvalidOperationException>();

        // Act
        var result = await _controller.GetForecastDetails("London");

        // Assert
        result.Should().BeOfType<ObjectResult>() 
            .Which.StatusCode.Should().Be(500); 
        _forecastService.Verify(x => x.GetForecast("London"), Times.Once);
    }
}