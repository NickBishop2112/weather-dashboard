using Microsoft.AspNetCore.Mvc;
using Serilog;
using WeatherDashboard.API.Interfaces;

namespace WeatherDashboard.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ForecastController(IForecastService forecastService) : ControllerBase
{
    /// <summary>
    /// Retrieves the weather forecast for a specific location.
    /// </summary>
    /// <param name="location">The location to get the forecast for.</param>
    /// <returns>The weather forecast details.</returns>
    /// <response code="200">Returns the weather forecast details.</response>
    /// <response code="400">If location is missing.</response>
    /// <response code="500">If an unexpected error occurs.</response>
    [HttpGet("{location}")]
    public async Task<IActionResult> GetForecastDetails(string location)
    {
        try
        {
            Log.Information($"Getting forecast for location: {location}");
            var weatherDetails = await forecastService.GetForecast(location);
            return Ok(weatherDetails);
        }
        catch (Exception e)
        {
            Log.Information($"Error has occurred: {e}");
            return StatusCode(500, new { message = $"An unexpected error has occurred: ${e.Message}" });
        }
    }
}