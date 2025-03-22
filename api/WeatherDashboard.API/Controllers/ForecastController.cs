using Microsoft.AspNetCore.Mvc;
using Serilog;
using WeatherDashboard.API.Interfaces;

namespace WeatherDashboard.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ForecastController(IForecastService forecastService) : ControllerBase
{
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