using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherDashboard.API.Controllers;
using WeatherDashboard.API.Interfaces;
using Xunit;
using Assert = Xunit.Assert;

namespace WeatherDashboard.API.Tests.Controllers;

public class DefaultLocationControllerTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IDefaultLocationService> _defaultLocationService;
    private readonly DefaultLocationController _controller;

    public DefaultLocationControllerTests()
    {
        _fixture = new Fixture();
        _defaultLocationService = new Mock<IDefaultLocationService>();
        _controller = new DefaultLocationController(_defaultLocationService.Object);
    }
    
    [Fact]
    public void SetLocation_DetailsReturned()
    {
        // Arrange
        var location = _fixture.Create<string>();
        
        // Act
        var result = _controller.SetLocation(location);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        _defaultLocationService.Verify(x => x.SetLocation(location), Times.Once);
    }
    
    [Fact]
    public void SetLocation_ErrorThrown()
    {
        // Arrange
        var location = _fixture.Create<string>();
        _defaultLocationService.Setup(x => x.SetLocation(location)).Throws<InvalidOperationException>();

        // Act
        var result = _controller.SetLocation(location);

        // Assert
        result.Should().BeOfType<ObjectResult>() 
            .Which.StatusCode.Should().Be(500); 
        _defaultLocationService.Verify(x => x.SetLocation(location), Times.Once);
    }
    
    [Fact]
    public void GetLocation_LocationReturned()
    {
        // Arrange
        var location = _fixture.Create<string>();
        _defaultLocationService.Setup(x => x.GetLocation()).Returns(location);
        
        // Act
        var result = _controller.GetLocation();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        _defaultLocationService.Verify(x => x.GetLocation(), Times.Once);
    }
    
    [Fact]
    public void GetLocation_ErrorThrown()
    {
        // Arrange
        _defaultLocationService.Setup(x => x.GetLocation()).Throws<InvalidOperationException>();
        
        // Act
        var result = _controller.GetLocation();

        // Assert
        result.Should().BeOfType<ObjectResult>() 
            .Which.StatusCode.Should().Be(500); 
        _defaultLocationService.Verify(x => x.GetLocation(), Times.Once);
    }
}